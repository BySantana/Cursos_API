using Cursos_API.Application.Dtos;
using System.Threading.Tasks;

namespace Cursos_API.Application.Interfaces
{
    public interface ILogService
    {
        Task<bool> AddLog(int cursoId, LogDto model);
        Task<bool> UpdateLog(int cursoId, LogDto model);

        //Task<bool> DeleteLog(int cursoId);

        Task<LogDto> GetLogByIdAsync(int logId);
        Task<LogDto> GetLogByCursoIdAsync(int cursoId);
        Task<LogDto[]> GetAllLogsAsync();
    }
}
