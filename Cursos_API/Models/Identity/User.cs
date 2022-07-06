using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace Cursos_API.Models.Identity
{
    public class User : IdentityUser<int>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public IEnumerable<Curso> Cursos { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
