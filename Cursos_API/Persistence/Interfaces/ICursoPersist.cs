using Cursos_API.Models;
using System;
using System.Threading.Tasks;

namespace Cursos_API.Persistence.Interfaces
{
    public interface ICursoPersist
    {
        Task<Curso> GetCursoByIdAsync(int cursoId);
        Task<Curso[]> GetAllCursosAsync();
        Task<Curso[]> GetAllCursosByUserIdAsync(int userId);
        Task<Curso[]> GetAllCursosByDataAsync(DateTime data);
        Task<Curso[]> GetAllCursosByDatasAsync(DateTime dataInicio, DateTime dataFinal);
        Task<Curso[]> GetCursoByTitulo(string titulo);
    }
}
