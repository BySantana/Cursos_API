using Cursos_API.Persistence.Contextos;
using Cursos_API.Persistence.Interfaces;
using System.Threading.Tasks;

namespace Cursos_API.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly Contexto _context;
        public GeralPersist(Contexto context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.AddAsync(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
