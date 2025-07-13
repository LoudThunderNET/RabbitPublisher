using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace RabbitPublisher.Logic.Models.Cfg
{
    public class RabbitHost
    {
        public static string Section = "Hosts";
        public string Name { get; set; }

        public string Url { get; set; }

        public string VHost { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
