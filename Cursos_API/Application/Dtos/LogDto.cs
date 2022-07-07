using System;

namespace Cursos_API.Application.Dtos
{
    public class LogDto
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public CursoDto Curso { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
