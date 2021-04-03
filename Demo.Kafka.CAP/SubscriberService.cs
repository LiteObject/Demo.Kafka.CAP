using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;

namespace Demo.Kafka.CAP
{
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        [CapSubscribe("xxx.services.show.time")]
        public void CheckReceivedMessage(DateTime datetime)
        {
        }
    }
}
