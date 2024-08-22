using Microsoft.AspNetCore.Identity;

namespace IdentityExercise1.Database
{
    public class User : IdentityUser
    {
        public string? Initials { get; set; }
    }
}
