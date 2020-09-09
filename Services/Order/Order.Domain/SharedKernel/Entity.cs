namespace Order.Domain.SharedKernel
{
    public abstract class Entity
    {
        int _Id;
        public virtual int Id
        {
            get 
            {
                return _Id;
            }

            protected set
            {
                _Id = value;
            }
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