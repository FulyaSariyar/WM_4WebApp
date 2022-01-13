using ItServiceApp.Models.Payment;
using System.Collections.Generic;

namespace ItServiceApp.Services
{
    public interface IPaymentService
    {
        public InstallmentModel CheckInstallments(string binNumber, decimal price);//bakıcaz!
        public PaymentResponseModel Pay(PaymentModel model);
    }
}
