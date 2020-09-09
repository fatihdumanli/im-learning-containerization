using Order.Domain.SharedKernel;

namespace Order.Domain.AggregatesModel.BuyerAggregate
{
    public class CardType : Enumeration
    {

        public static CardType Amex = new CardType(1, "Amex");
        public static CardType Visa = new CardType(2, "Visa");
        public static CardType MasterCard = new CardType(3, "MasterCard");
  
        public CardType(int id, string name)
            : base(id, name)
        {            
        }
    }
}