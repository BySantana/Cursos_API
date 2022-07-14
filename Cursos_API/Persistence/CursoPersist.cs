using Cursos_API.Models;
using Cursos_API.Persistence.Contextos;
using Cursos_API.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cursos_API.Persistence
{
    public class CursoPersist : ICursoPersist
    {
        private readonly Contexto _context;
        public CursoPersist(Contexto context)
        {
            _context = context;
        }
        public async Task<Curso[]> GetAllCursosAsync()
        {
            IQueryable<Curso> query = _context.Cursos
                .Include(i => i.Categoria)
                .Include(i => i.User);

            query = query.Where(w => w.Status == true);

            return await query.ToArrayAsync();
        }

        public async Task<Curso[]> GetAllCursosByDataAsync(DateTime data)
        {
            IQueryable<Curso> query = _context.Cursos
                .Include(i => i.Categoria)
                .Include(i => i.User);

            query = query.Where(w => w.DataInicio <= data &&
                                w.DataTermino >= data &&
                                w.Status == true);

            return await query.ToArrayAsync();
        }

        public async Task<Curso[]> GetAllCursosByDatasAsync(DateTime dataInicio, DateTime dataFinal)
        {
            IQueryable<Curso> query = _context.Cursos
                .Include(i => i.Categoria)
                .Include(i => i.User);

            query = query.Where(w => ((dataInicio >= w.DataInicio && dataInicio <= w.DataTermino) ||
                                     (dataFinal >= w.DataInicio && dataFinal <= w.DataTermino) ||
                                     (w.DataInicio >= dataInicio && w.DataInicio <= dataFinal)) &&
                                     (w.Status == true));

            return await query.ToArrayAsync();
        }

        

        public async Task<Curso[]> GetAllCursosByUserIdAsync(int userId)
        {
            IQueryable<Curso> query = _context.Cursos
                .Include(i => i.Categoria)
                .Include(i => i.User);

            query = query.Where(w => w.UserId == userId &&
                                w.Status == true);

            return await query.ToArrayAsync();
        }

        public async Task<Curso> GetCursoByIdAsync(int cursoId)
        {
            IQueryable<Curso> query = _context.Cursos;
                //.Include(i => i.Categoria);

            query = query.Where(w => w.CursoId == cursoId &&
                                w.Status == true);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Curso[]> GetCursoByTitulo(string titulo)
        {
            IQueryable<Curso> query = _context.Cursos;

            query = query.Where(w => w.Descricao.ToLower() == titulo.ToLower() &&
                                     w.Status == true);

            return await query.ToArrayAsync();
        }
    }
}
