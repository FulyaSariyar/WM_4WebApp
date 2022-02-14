using ItServiceApp.Core.ComplexTypes;
using ItServiceApp.Models;
using System.Threading.Tasks;

namespace ItServiceApp.Business.Services.Email
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message);
    }
}
