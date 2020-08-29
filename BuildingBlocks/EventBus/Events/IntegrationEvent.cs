using System;
using Newtonsoft.Json;

namespace EventBus.Events
{
    public class IntegrationEvent
    {
        [JsonProperty]
        public Guid Id { get; set; }

        [JsonProperty]
        public DateTime CreationDate { get; set; }


        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        [JsonConstructor]        
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;            
        }
    }

}