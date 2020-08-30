using System;
using EventBus.Abstractions;

namespace EventBus
{
    public class Subscription
    {
        public string EventName { get; set; }
        public Type EventHandler { get; set; }

        public Subscription(string evtName, Type evtHandler)
        {
            this.EventHandler = evtHandler;
            this.EventName = evtName;           
        }
    }
}