using EventBus.Events;
using Newtonsoft.Json;
using System;

namespace IntegrationEventLog
{
    public class IntegrationEventLogEntry
    {
        private IntegrationEventLogEntry() { }
        public Guid EventId { get; private set; }
        public string EventTypeName { get; private set; }
        public IntegrationEvent IntegrationEvent { get; private set; }  
                   
        public EventStateEnum State { get; set; }
        public DateTime CreationTime { get; private set; }
        public string Content { get; private set; }
        public string TransactionId { get; private set; }

        public IntegrationEventLogEntry(IntegrationEvent integrationEvent,
            Guid transactionId)
        {
            EventId = integrationEvent.Id;
            CreationTime = integrationEvent.CreationDate;
            EventTypeName = integrationEvent.GetType().Name;
            Content = JsonConvert.SerializeObject(integrationEvent);
            TransactionId = transactionId.ToString();
        }

        public IntegrationEventLogEntry DeserializeJsonContent(Type type)
        {
            IntegrationEvent = JsonConvert.DeserializeObject(Content, type) as IntegrationEvent;
            return this;
        }


    }
}