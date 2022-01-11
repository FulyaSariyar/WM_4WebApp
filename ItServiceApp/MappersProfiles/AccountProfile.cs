using AutoMapper;
using ItServiceApp.Models.Identity;
using ItServiceApp.ViewModels;

namespace ItServiceApp.MappersProfiles
{
    public class AccountProfile:Profile
    {
        public AccountProfile()
        {
            CreateMap<ApplicationUser, UserProfileViewModel>().ReverseMap();
        }

    }
}
