using MassTransit;
using Newtonsoft.Json;
using RabbitPublisher.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class SampleMessageConsumer : IConsumer<SampleMessage>
    {
        public Task Consume(ConsumeContext<SampleMessage> context)
        {
            Console.WriteLine($"Пришло сообщение: {JsonConvert.SerializeObject(context.Message)}");

            return Task.CompletedTask;
        }
    }
}
