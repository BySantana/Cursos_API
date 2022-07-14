using System;
using System.Globalization;

namespace Cursos_API.Application.Dtos
{
    public class CursoDto
    {
        public int CursoId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int QtdAlunos { get; set; }
        public bool Status { get; set; }
        public int CategoriaId { get; set; }
        public CategoriaDto Categoria { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
