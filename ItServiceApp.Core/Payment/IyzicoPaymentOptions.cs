using Iyzipay;

namespace ItServiceApp.Core
{
    public class IyzicoPaymentOptions:Options
    {
        public const string Key = "IyzicoOptions";
        public string ThreedsCallbackUrl  { get; set; }

    }
}
