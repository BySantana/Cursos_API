using Cursos_API.Models.Identity;
using System;

namespace Cursos_API.Models
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int QtdAlunos { get; set; }
        public bool Status { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
