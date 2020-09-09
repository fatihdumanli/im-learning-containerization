namespace Order.Domain.SharedKernel
{
    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if(ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
            {
                return false;
            }
        }


    }
}