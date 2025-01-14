using Microsoft.AspNetCore.Identity;

namespace RazorPagesLibraryProject.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public DateTime CreatedAt { get; set; }

    }
}
