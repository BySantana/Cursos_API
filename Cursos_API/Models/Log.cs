using Cursos_API.Models.Identity;
using System;

namespace Cursos_API.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int CursoId { get; set; }    
        public Curso Curso { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
