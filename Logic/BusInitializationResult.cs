using MassTransit;

namespace Rabbit.Publisher.Logic
{
    public class BusInitializationResult
    {
        public IBusControl Bus { get; set; }

        public string Error { get; set; }
    }
}
