using Cursos_API.Models.Identity;
using System.Threading.Tasks;

namespace Cursos_API.Persistence.Interfaces
{
    public interface IUserPersist : IGeralPersist
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}
