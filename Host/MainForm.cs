using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitPublisher.Logic.Models.Cfg;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace RabbitPublished
{
    public partial class MainForm : Form
    {
        private TreeNode? currentItem { get; set; }

        private const string NoContracsDefined = "Не заданы контракты сообщений";
        private Dictionary<string, List<Type>> namespaceToTypeList = new Dictionary<string, List<Type>>();
        private readonly IOptions<IEnumerable<RabbitHost>> _hosts;
        private readonly IOptions<IEnumerable<Contract>> _contracts;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly ILogger<MainForm> _logger;
        private List<string> _errors = new List<string>();
        private string? messagesDir = null;
        private const int DllImageIndex = 3;
        private const int NamespaceImageIndex = 4;
        private const int ClassImageIndex = 5;
        public MainForm(
            IHostApplicationLifetime hostApplicationLifetime,
            IOptions<List<RabbitHost>> hosts,
            IOptions<List<Contract>> contracts,
            ISendEndpointProvider sendEndpointProvider,
            ILogger<MainForm> logger)
        {
            InitializeComponent();
            _hosts = hosts;
            _contracts = contracts;
            _sendEndpointProvider = sendEndpointProvider;
            _logger = logger;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            posStatusLblValue.Alignment = ToolStripItemAlignment.Right;
            posStatusLbl.Alignment = ToolStripItemAlignment.Right;
            colStatusLblValue.Alignment = ToolStripItemAlignment.Right;
            colStatusLbl.Alignment = ToolStripItemAlignment.Right;
            lineStatusLblValue.Alignment = ToolStripItemAlignment.Right;
            lineStatusLbl.Alignment = ToolStripItemAlignment.Right;
            messagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Messages");
            if (!Directory.Exists(messagesDir))
                Directory.CreateDirectory(messagesDir);

            var contracts = _contracts.Value;
            if (contracts.Count() == 0)
            {
                _errors.Add(NoContracsDefined);
                _logger.LogWarning(NoContracsDefined);
            }
            else
            {
                bool setFirstHost = false;
                foreach (RabbitHost rabbitHost in _hosts.Value)
                {
                    if (!string.IsNullOrWhiteSpace(rabbitHost.Name))
                    {
                        cbRabbitHosts.Items.Add(rabbitHost);
                        if (!setFirstHost)
                        {
                            cbRabbitHosts.SelectedIndex = 0;
                            setFirstHost = true;
                        }
                    }
                }
                AppDomain.CurrentDomain.AssemblyResolve += currentDomain_AssemblyResolve;
                foreach (Contract contract in contracts)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(contract.FileName))
                            continue;

                        var assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, contract.FileName));
                        IEnumerable<Type> source = assembly.GetExportedTypes()
                            .Where(t => !t.IsAbstract && !t.IsArray && !t.IsEnum && !t.IsInterface && !t.IsGenericType);

                        var nsPrefix = Path.GetFileNameWithoutExtension(contract.FileName);
                        // сборки
                        TreeNode dllNode = new(nsPrefix, DllImageIndex, DllImageIndex);
                        treeView1.Nodes.Add(dllNode);

                        var rootItems = source
                            .GroupBy(t => t.Namespace!)
                            .Where(n => n.Key.Length == nsPrefix.Length)
                            .SelectMany(n => n.ToList())
                            .ToArray();

                        foreach (var @namespace in source.Where(t => t.Namespace!.Length > nsPrefix.Length).GroupBy(NamespaceSelector))
                        {
                            var classes = @namespace
                                .Select(cls =>
                                {
                                    var classNode = new TreeNode(cls.Name, ClassImageIndex, ClassImageIndex);
                                    classNode.Tag = cls;

                                    return classNode;
                                })
                                .ToArray();

                            dllNode.Nodes.Add(new TreeNode(@namespace.Key, NamespaceImageIndex, NamespaceImageIndex, classes));
                        }

                        if (rootItems.Length > 0)
                        {
                            dllNode.Nodes.AddRange(rootItems
                                .Select(cls =>
                                {
                                    // classes
                                    var node = new TreeNode(cls.Name, ClassImageIndex, ClassImageIndex);
                                    node.Tag = cls;

                                    return node;
                                })
                                .ToArray());
                        }

                        string NamespaceSelector(Type t) =>
                            t.Namespace!.StartsWith(nsPrefix)
                            ? t.Namespace!.Substring(nsPrefix.Length + 1) :
                            t.Namespace!;
                    }
                    catch (Exception ex)
                    {
                        string message = $"Загрузка сборки {contract.FileName} завершилась ошибкой: {ex.Message}";
                        _errors.Add(message);
                        _logger.LogError(ex, message);
                    }


                }
                if (treeView1.Nodes.Count == 1)
                    treeView1.Nodes[0].Expand();

                if (_errors.Count == 0)
                {
                    return;
                }

                var errorMessage = string.Join(Environment.NewLine, _errors);
                int num = (int)MessageBox.Show(errorMessage, "При загрузки приложения произошли ошибки.");
            }
        }

        private (string? Message, bool CanSend) GetEmptySerializedMessage(bool deepInitialization)
        {
            if (currentItem == null)
                return (null, false);

            var type = FindType(currentItem.Tag.ToString());
            if (type == null)
            {
                return (string.Empty, false);
            }

            object instance1 = Activator.CreateInstance(type);
            if (deepInitialization)
            {
                foreach (PropertyInfo runtimeProperty in instance1.GetType().GetRuntimeProperties())
                {
                    if (runtimeProperty.CanWrite && !runtimeProperty.PropertyType.IsValueType && !runtimeProperty.PropertyType.IsInterface && runtimeProperty.PropertyType.Name != "String")
                    {
                        if (runtimeProperty.PropertyType.IsArray)
                        {
                            InitializeArray(instance1, runtimeProperty);
                        }
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
                                _logger.LogWarning(ex, $"Создание экземпляра {runtimeProperty.PropertyType.FullName} завершилась ошибкой: {ex.Message}");
                            }
                        }
                    }
                }
            }
            return (JsonConvert.SerializeObject(instance1, formatting: Formatting.Indented), true);
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
                    methodInfo?.Invoke(instance1, [instance2, 0]);
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
                    methodInfo?.Invoke(instance1, [instance2, 0]);
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
            currentItem.ImageIndex = currentItem.SelectedImageIndex = 0;

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
                currentItem.ImageIndex = currentItem.SelectedImageIndex = 1;
                SaveMessageToFile(currentItem.Text);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, "Ошибка");
                currentItem.ImageIndex = currentItem.SelectedImageIndex = 2;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (currentItem != null)
            {
                SaveMessageToFile(currentItem.Text);
            }
        }

        private void cbRabbitHosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox combobox && combobox.SelectedIndex < 0)
                return;

            btnSend.Enabled = true;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            //if (SearchTextBox.Text == "")
            //    FillTypesListView(namespaceToTypeList);
            //else
            //    FillTypesListView(namespaceToTypeList.SelectMany(i => i.Value.Where(v => v.Name.ToLowerInvariant().Contains(SearchTextBox.Text.ToLower())))
            //        .GroupBy(t => t.Namespace!)
            //        .ToDictionary(g => g.Key, g => g.ToList()));
        }

        private Type? FindType(string? qualifiedTypeName)
        {
            if (qualifiedTypeName == null)
                return null;

            var messageType = Type.GetType(qualifiedTypeName);
            if (messageType != null)
                return NullIfGeneric(messageType);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                messageType = assembly.GetType(qualifiedTypeName);
                if (messageType != null)
                    return NullIfGeneric(messageType);
            }
            return null;

            Type? NullIfGeneric(Type type)
            {
                if (type.IsGenericType)
                {
                    return null;
                }

                return type;
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
            var (message, canSend) = GetEmptySerializedMessage(chkDeepInitialization.Checked);
            messageTextBox.Text = message;
            btnSave.Enabled = canSend;
            btnSend.Enabled = canSend;
        }

        private Assembly? currentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
        {
            if (args.RequestingAssembly == null)
                return null;

            var assemblyName = args.RequestingAssembly.GetReferencedAssemblies()
                .FirstOrDefault(r => r.FullName == args.Name);
            if (args.RequestingAssembly != null)
            {
                string? contractsDirectory = Path.GetDirectoryName(args.RequestingAssembly.Location);
                if (!string.IsNullOrEmpty(contractsDirectory))
                {
                    return assemblyName != null
                        ? Assembly.LoadFile(Path.Combine(contractsDirectory, assemblyName.Name + ".dll"))
                        : null;
                }
            }

            return null;
        }


        private void messageTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is TextBox messagetxtBox)
            {
                posStatusLblValue.Text = messagetxtBox.SelectionStart.ToString();
                int start = 0, end = 0;
                for (int i = 0; i < messagetxtBox.Lines.Length; i++)
                {
                    end = start + messagetxtBox.Lines[i].Length + ((i != messagetxtBox.Lines.Length - 1) ? 1 : 0);
                    int sel = messagetxtBox.SelectionStart;
                    if (start <= sel && sel <= end)
                    {
                        lineStatusLblValue.Text = (i + 1).ToString();
                        colStatusLblValue.Text = (messagetxtBox.SelectionStart - start + 1).ToString();
                    }
                    start = end + 1;
                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (sender is not TreeView treeView)
            {
                return;
            }

            var node = e.Node;
            if (node == null)
            {
                return;
            }

            if (node.Tag == null)
            {
                btnNew.Enabled = false;
                return;
            }

            btnNew.Enabled = true;
            if (currentItem != null && treeView.Text != currentItem.Text)
            {
                SaveMessageToFile(currentItem.Text);
            }

            messageTextBox.Clear();
            if (currentItem != null) 
            { 
                if(currentItem.SelectedImageIndex == 2)
                {
                    currentItem.ForeColor = Color.Red;
                    currentItem.ImageIndex = currentItem.SelectedImageIndex = ClassImageIndex;
                }
                else if(currentItem.SelectedImageIndex == 1)
                {
                    currentItem.ForeColor = Color.Green;
                    currentItem.ImageIndex = currentItem.SelectedImageIndex = ClassImageIndex;
                }
            }

            currentItem = node;
            if (LoadMessageFromFile(currentItem.Text))
            {
                return;
            }
            var (message, canSend) = GetEmptySerializedMessage(chkDeepInitialization.Checked);
            messageTextBox.Text = message;
            btnSend.Enabled = canSend;
            btnSave.Enabled = canSend;

        }
    }
}
