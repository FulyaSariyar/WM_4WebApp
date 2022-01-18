using ItServiceApp.Models.Identity;
using ItServiceApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItServiceApp.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public UserApiController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users.OrderBy(x =>
              x.CreatedDate).ToList();

            return Ok(users);
        }

        [HttpGet]
        public IActionResult GetTest()
        {

            var users = new List<UserProfileViewModel>();
            for (int i=0; i<10000; i++)
            {
                users.Add(new()
                {
                    Email="Deneme"+i,
                    Surname="soyad"+i,
                    Name="ad"+1
                });
                
            }
            return Ok(users);
        }

    }
}
