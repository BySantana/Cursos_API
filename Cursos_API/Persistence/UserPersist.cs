using Cursos_API.Models.Identity;
using Cursos_API.Persistence.Contextos;
using Cursos_API.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cursos_API.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly Contexto _context;
        public UserPersist(Contexto context) : base(context)
        {
            _context = context;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(
                user => user.UserName.Contains(userName.ToLower()));
        }
    }
}
