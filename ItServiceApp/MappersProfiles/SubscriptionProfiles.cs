using AutoMapper;
using ItServiceApp.Models.Entities;
using ItServiceApp.ViewModels;

namespace ItServiceApp.MappersProfiles
{
    public class SubscriptionProfiles: Profile
    {
        public SubscriptionProfiles()
        {
            CreateMap<SubscriptionType, SubscriptionsViewModel>().ReverseMap();
        }
    }
}
