using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitPublisher.Logic
{
    public enum ImageType
    {
        None = -1,
        Error = 0,
        Success = 1,
        Sending = 2,
    }
}
