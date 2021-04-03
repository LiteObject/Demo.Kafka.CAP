using System;

namespace Demo.Kafka.CAP
{
    public interface ISubscriberService
    {
        void CheckReceivedMessage(DateTime datetime);
    }
}
