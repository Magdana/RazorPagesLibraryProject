using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesLibraryProject.Entities;

namespace RazorPagesLibraryProject.Pages
{
    [Authorize]
    public class UserPageModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;
        public UserEntity? appUser;
        public UserPageModel(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }
        public void OnGet()
        {
            var task = _userManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;
        }
    }
}
