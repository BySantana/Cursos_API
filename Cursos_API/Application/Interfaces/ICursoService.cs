using Cursos_API.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace Cursos_API.Application.Interfaces
{
    public interface ICursoService
    {
        Task<CursoDto> AddCurso(int userId, CursoDto model);
        Task<CursoDto> UpdateCurso(int userId, int cursoId, CursoDto model);
        Task<bool> DeleteCurso(int userId, int cursoId);


        Task<CursoDto> GetCursoByIdAsync(int cursoId);
        Task<CursoDto[]> GetAllCursosAsync();
        Task<CursoDto[]> GetAllCursosByUserIdAsync(int userId);
        Task<CursoDto[]> GetAllCursosByDataAsync(DateTime data);

    }
}
