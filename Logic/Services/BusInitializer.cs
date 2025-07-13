using MassTransit;
using Microsoft.Extensions.Configuration;
using RabbitPublisher.Logic.Models.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitPublisher.Logic.Services
{
    public static class BusInitializer
    {
        private static readonly Dictionary<string, IBusControl> _buses = new Dictionary<string, IBusControl>();

        //public static Dictionary<string, IBusControl> InitBuses(IConfiguration configuration)
        //{
        //    configuration.GetSection(RabbitHost.Section).Bind(o)
        //    var hosts = _hosts.Value;
        //    if (hosts.Count() == 0)
        //    {
        //        _errors.Add("Не заданы хосты Rabbit`а.");
        //    }
        //}
    }
}
