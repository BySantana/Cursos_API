using Cursos_API.Application.Dtos;
using System.Threading.Tasks;

namespace Cursos_API.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}
