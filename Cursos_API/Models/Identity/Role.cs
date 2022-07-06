using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Cursos_API.Models.Identity
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
