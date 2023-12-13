
using Microsoft.AspNetCore.Identity;

namespace AppCloud
{
    public class AppUser : IdentityUser
    {
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public ICollection<FileModel> Files { get; set; }
    }
}
