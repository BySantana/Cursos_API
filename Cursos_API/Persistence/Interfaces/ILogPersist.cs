using Cursos_API.Models;
using System.Threading.Tasks;

namespace Cursos_API.Persistence.Interfaces
{
    public interface ILogPersist
    {
        Task<Log> GetLogByIdAsync(int logId);
        Task<Log[]> GetLogsByCursoIdAsync(int cursoId);
        Task<Log[]> GetAllLogsAsync();

    }
}
