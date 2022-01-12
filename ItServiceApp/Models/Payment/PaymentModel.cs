using System.Collections.Generic;

namespace ItServiceApp.Models.Payment
{
    public class PaymentModel
    {
        public string PaymentId  { get; set; }
        public string Price  { get; set; }
        public string PaidPrice { get; set; }
        public string Installment { get; set; }
        public CardModel CardModel { get; set; }
        public List<BasketModel> BasketList { get; set; }
        public CustomerModel Customer { get; set; }
        public AddressModel Address { get; set; }
    }
}
