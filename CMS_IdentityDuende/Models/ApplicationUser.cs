using Microsoft.AspNetCore.Identity;

namespace CMS_IdentityDuende.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
