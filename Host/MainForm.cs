using MassTransit;
using MassTransit.Configuration;
using MassTransit.RabbitMqTransport;
using MassTransit.Transports;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rabbit.Publisher.Logic;
using RabbitPublisher.Logic.Models.Cfg;
using System.Configuration;
using System.Reflection;
using System.Xml;
using static MassTransit.Logging.DiagnosticHeaders.Messaging;

namespace RabbitPublished
{
    public partial class MainForm : Form
    {
        private ListViewItem? currentItem { get; set; }
        private Dictionary<string, List<Type>> namespaceToTypeList = new Dictionary<string, List<Type>>();
        private readonly IOptions<IEnumerable<RabbitHost>> _hosts;
        private readonly IOptions<IEnumerable<Contract>> _contracts;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISendEndpoint _sendEndpoint;
        private readonly IPublishEndpointProvider _publishEndpointProvider;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private List<string> _errors = new List<string>();
        private string? contractsDir = null;
        private string? messagesDir = null;

        public MainForm(
            IHostApplicationLifetime hostApplicationLifetime,
            IOptions<List<RabbitHost>> hosts,
            IOptions<List<Contract>> contracts,
            ISendEndpointProvider sendEndpointProvider)
        {
            InitializeComponent();
            _hosts = hosts;
            _contracts = contracts;
            _sendEndpointProvider = sendEndpointProvider;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            messagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Messages");
            if (!Directory.Exists(messagesDir))
                Directory.CreateDirectory(messagesDir);

            var contracts = _contracts.Value;
            if (contracts.Count() == 0)
            {
                _errors.Add("Не заданы контракты сообщений.");
            }

            else
            {
                bool setFirstHost = false;
                foreach (RabbitHost rabbitHost in _hosts.Value)
                {
                    //    BusInitializationResult initializationResult = MainForm.InitializeBus(rabbitHost);
                    //    _buses[rabbitHost.Name] = initializationResult.Bus;
                    if (!string.IsNullOrWhiteSpace(rabbitHost.Name))
                    {
                        cbRabbitHosts.Items.Add(rabbitHost);
                        if (!setFirstHost)
                        {
                            cbRabbitHosts.SelectedIndex = 0;
                            setFirstHost = true;
                        }
                    }
                    //    else
                    //                        _errors.Add(initializationResult.Error);
                }
                var source = new List<Type>();
                AppDomain.CurrentDomain.AssemblyResolve += currentDomain_AssemblyResolve;
                foreach (Contract contract in contracts)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(contract.FileName))
                            continue;

                        var assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, contract.FileName));
                        source.AddRange(assembly.GetExportedTypes().Where(t => !t.IsAbstract && !t.IsArray && !t.IsEnum && !t.IsInterface));
                    }
                    catch (Exception ex)
                    {
                        _errors.Add(ex.Message);
                    }
                }
                namespaceToTypeList = source.GroupBy(t => t.Namespace!)
                    .ToDictionary(g => g.Key, g => g.ToList());
                FillTypesListView(namespaceToTypeList);

                if (_errors.Count == 0)
                    return;

                var errorMessage = string.Join(Environment.NewLine, _errors);
                int num = (int)MessageBox.Show(errorMessage, "При загрузки приложения произошли ошибки.");
                File.WriteAllText("MessagePublisher_errors.txt", errorMessage);
            }
        }

        private static BusInitializationResult InitializeBus(RabbitHost defaultHostCfg)
        {
            string url1 = defaultHostCfg.Url;
            string login = defaultHostCfg.Login;
            string pass = defaultHostCfg.Password;
            string virtualHost = defaultHostCfg.VHost;

            string url = !string.IsNullOrWhiteSpace(virtualHost) ? $"rabbitmq://{url1}/{virtualHost}" : "rabbitmq://" + url1;
            string str = (string)null;
            IBusControl usingRabbitMq = Bus.Factory.CreateUsingRabbitMq(cfg => cfg.Host(new Uri(url), h =>
            {
                h.Username(login);
                h.Password(pass);
            }));
            try
            {
                usingRabbitMq.Start();
            }
            catch (Exception ex)
            {
                str = ex.ToString();
            }

            return new BusInitializationResult()
            {
                Bus = usingRabbitMq,
                Error = str
            };
        }

        private void messageTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(sender is ListView listView) || listView.SelectedItems.Count != 1)
            {
                btnNew.Enabled = false;
            }
            else
            {
                btnNew.Enabled = true;
                ListViewItem selectedItem = listView.SelectedItems[0];
                if (currentItem != null && selectedItem.Text != currentItem.Text)
                    SaveMessageToFile(currentItem.Text);
                messageTextBox.Clear();
                currentItem = selectedItem;
                if (LoadMessageFromFile(currentItem.Text))
                    return;
                messageTextBox.Text = GetEmptySerializedMessage(chkDeepInitialization.Checked);
            }
        }

        private string GetEmptySerializedMessage(bool deepInitialization)
        {
            if (currentItem == null)
                return null;

            object instance1 = Activator.CreateInstance(FindType(currentItem.Tag.ToString()));
            if (deepInitialization)
            {
                foreach (PropertyInfo runtimeProperty in instance1.GetType().GetRuntimeProperties())
                {
                    if (runtimeProperty.CanWrite && !runtimeProperty.PropertyType.IsValueType && !runtimeProperty.PropertyType.IsInterface && runtimeProperty.PropertyType.Name != "String")
                    {
                        if (runtimeProperty.PropertyType.IsArray)
                            InitializeArray(instance1, runtimeProperty);
                        else if (IsList(runtimeProperty.PropertyType))
                        {
                            InitializeList(instance1, runtimeProperty);
                        }
                        else
                        {
                            runtimeProperty.GetSetMethod();
                            try
                            {
                                object instance2 = Activator.CreateInstance(runtimeProperty.PropertyType);
                                runtimeProperty.SetValue(instance1, instance2);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
            }
            return JsonConvert.SerializeObject(instance1, Newtonsoft.Json.Formatting.Indented);
        }

        private void InitializeList(object instance, PropertyInfo prop)
        {
            object instance1 = Activator.CreateInstance(prop.PropertyType, 1);
            Type type1 = prop.PropertyType.GetInterface("IList`1");
            if (type1 != null)
            {
                Type type2 = type1.GetGenericArguments().FirstOrDefault();
                if (type2 != null && MainForm.IsReferenceType(type2) && !type2.IsGenericType)
                {
                    object instance2 = Activator.CreateInstance(type2);
                    MethodInfo methodInfo = prop.PropertyType.BaseType.GetMethods().FirstOrDefault(m => m.Name == "SetValue" && m.GetParameters().Length == 2);
                    if (methodInfo != null)
                        methodInfo.Invoke(instance1, [instance2, 0]);
                }
            }
            prop.SetValue(instance, instance1);
        }

        private bool IsList(Type exporedType) => !(exporedType.GetInterface("IList`1") == (Type)null);

        private void InitializeArray(object instance, PropertyInfo prop)
        {
            object instance1 = Activator.CreateInstance(prop.PropertyType, 1);
            Type? type1 = prop.PropertyType.GetInterface("IEnumerable`1");
            if (type1 != null)
            {
                Type? type2 = type1.GetGenericArguments().FirstOrDefault();
                if (type2 != null && MainForm.IsReferenceType(type2) && !type2.IsGenericType)
                {
                    object? instance2 = Activator.CreateInstance(type2);
                    MethodInfo? methodInfo = prop.PropertyType.BaseType.GetMethods().FirstOrDefault(m => m.Name == "SetValue" && m.GetParameters().Length == 2);
                    if (methodInfo != null)
                        methodInfo.Invoke(instance1, [instance2, 0]);
                }
            }
            prop.SetValue(instance, instance1);
        }

        private static bool IsReferenceType(Type exploredType)
        {
            return !exploredType.IsValueType && !exploredType.IsInterface && exploredType.Name != "String";
        }

        private bool LoadMessageFromFile(string fileName)
        {
            string path = Path.Combine(messagesDir, fileName + ".json");
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(path))
                return false;
            messageTextBox.Text = File.ReadAllText(path);
            return true;
        }

        private void SaveMessageToFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(messageTextBox.Text))
                return;
            File.WriteAllText(Path.Combine(messagesDir, fileName + ".json"), messageTextBox.Text);
            btnSave.Enabled = false;
        }

        private async void sendButton_Click(object sender, EventArgs e)
        {
            if (currentItem == null || messageTextBox.TextLength == 0)
                return;
            currentItem.ImageIndex = 2;

            Type? selectedType = FindType(currentItem.Tag?.ToString());
            if (selectedType == null)
            {
                MessageBox.Show($"не удалось десериализовать сообщение в тип {currentItem.Tag?.ToString()}");
                return;
            }

            try
            {
                object? message = JsonConvert.DeserializeObject(messageTextBox.Text, selectedType);
                if (message == null)
                {
                    MessageBox.Show($"не удалось десериализовать сообщение в тип {selectedType.FullName}");
                    return;
                }
                RabbitHost hostCfg = (cbRabbitHosts.SelectedItem as RabbitHost)!;

                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"rabbitmq://{hostCfg.Url}/{message.GetType().Name}"));
                await sendEndpoint.Send(message);
                //await _buses[hostCfg!.Name].Publish(message, selectedType);
                currentItem.ImageIndex = 1;
                SaveMessageToFile(currentItem.Text);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, "Ошибка");
                currentItem.ImageIndex = 0;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (currentItem != null)
                SaveMessageToFile(currentItem.Text);

            //foreach (KeyValuePair<string, IBusControl> bus in _buses)
            //    bus.Value.Stop();
        }

        private void cbRabbitHosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox combobox && combobox.SelectedIndex < 0)
                return;

            sendButton.Enabled = true;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SearchTextBox.Text == "")
                FillTypesListView(namespaceToTypeList);
            else
                FillTypesListView(namespaceToTypeList.SelectMany(i => i.Value.Where(v => v.Name.ToLowerInvariant().Contains(SearchTextBox.Text.ToLower())))
                    .GroupBy(t => t.Namespace!)
                    .ToDictionary(g => g.Key, g => g.ToList()));
        }

        private Type? FindType(string? qualifiedTypeName)
        {
            if (qualifiedTypeName == null)
                return null;

            var messageType = Type.GetType(qualifiedTypeName);
            if (messageType != null)
                return messageType;

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                messageType = assembly.GetType(qualifiedTypeName);
                if (messageType != null)
                    return messageType;
            }
            return null;
        }

        private void FillTypesListView(Dictionary<string, List<Type>> typeList)
        {
            messageTypesListView.Items.Clear();
            messageTypesListView.Groups.Clear();
            foreach (KeyValuePair<string, List<Type>> type in typeList)
            {
                ListViewGroup listViewGroup = new ListViewGroup()
                {
                    Name = type.Key,
                    Header = type.Key
                };
                messageTypesListView.Groups.Add(listViewGroup);
                messageTypesListView.Items.AddRange(type.Value.Select(i => new ListViewItem()
                {
                    Text = i.Name,
                    Tag = i.FullName,
                    Group = listViewGroup
                }).ToArray<ListViewItem>());
            }
        }

        private void clearSearchButton_Click(object sender, EventArgs e) => SearchTextBox.Text = "";

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (_errors.Count <= 0)
                return;
            string text = string.Join(",", (IEnumerable<string>)_errors);
            _errors.Clear();
            int num = (int)MessageBox.Show(text);
        }

        private void messageTextBox_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentItem == null)
            {
                MessageBox.Show("Не выбран элемент");
                return;
            }

            SaveMessageToFile(currentItem.Text);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Операция перезапишет изменения по текущему сообщению. Продолжить ?", "Генерация нового сообщения", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            messageTextBox.Text = GetEmptySerializedMessage(chkDeepInitialization.Checked);
        }

        private Assembly? currentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
        {
            if (args.RequestingAssembly == null)
                return null;

            var assemblyName = args.RequestingAssembly.GetReferencedAssemblies()
                .FirstOrDefault(r => r.FullName == args.Name);

            return assemblyName != null
                ? Assembly.LoadFile(Path.Combine(Path.GetDirectoryName(new Uri(args.RequestingAssembly.GetName().Name!).AbsolutePath), assemblyName.Name + ".dll"))
                : null;
        }

        private void controlPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
