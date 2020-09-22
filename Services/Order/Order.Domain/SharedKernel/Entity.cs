using System;
using System.Collections.Generic;
using DomainDispatching.DomainEvent;
using MediatR;

namespace Ordering.Domain.SharedKernel
{
    public abstract class Entity
    {
        int _id;
        public virtual int Id
        {
            get 
            {
                return _id;
            }

            protected set
            {
                _id = value;
            }
        }      
        protected List<INotification> _domainEvents;
        
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
        
        protected void AddDomainEvent(INotification domainEvent) 
        {
            if(_domainEvents == null)
            {
                _domainEvents = new List<INotification>();
            }

            _domainEvents.Add(domainEvent);
        }
        
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        
        public bool IsTransient() 
        {
            return this.Id == default(Int32);
        }
        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is Entity))
                return false;

            if(object.ReferenceEquals(this, obj))
                return true;

            if(this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity) obj;

            if(item.IsTransient() || this.IsTransient())
                return false;        

            else 
                return item.Id == this.Id;      
        }       
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new System.NotImplementedException();
            return base.GetHashCode();
        }

    }
}