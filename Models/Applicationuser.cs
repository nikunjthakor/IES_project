using Microsoft.AspNetCore.Identity;

namespace IES_project.Models
{
    public class Applicationuser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
