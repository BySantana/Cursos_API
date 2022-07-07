using Cursos_API.Models;
using Cursos_API.Persistence.Contextos;
using Cursos_API.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cursos_API.Persistence
{
    public class LogPersist : ILogPersist
    {
        private readonly Contexto _context;
        public LogPersist(Contexto context)
        {
            _context = context;
        }

        public async Task<Log[]> GetAllLogsAsync()
        {
            IQueryable<Log> query = _context.Logs
                .Include(l => l.Curso);


            return await query.ToArrayAsync();
        }

        public async Task<Log> GetLogByIdAsync(int logId)
        {
            IQueryable<Log> query = _context.Logs
                .Include(l => l.Curso);

            query = query.Where(x => x.Id == logId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Log> GetLogByCursoIdAsync(int cursoId)
        {
            IQueryable<Log> query = _context.Logs;
                //.Include(l => l.Curso);

            query = query.Where(x => x.CursoId == cursoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
