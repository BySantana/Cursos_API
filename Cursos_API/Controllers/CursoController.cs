using Cursos_API.Application.Dtos;
using Cursos_API.Application.Interfaces;
using Cursos_API.Controllers.Extensions;
using Cursos_API.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Cursos_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;
        private readonly IGeralPersist _geralPersist;

        public CursoController(ICursoService cursoService, IGeralPersist geralPersist)
        {
            _cursoService = cursoService;
            _geralPersist = geralPersist;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cursos = await _cursoService.GetAllCursosAsync();
                if (cursos == null) return NoContent();

                return Ok(cursos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Cursos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var curso = await _cursoService.GetCursoByIdAsync(id);
                if (curso == null) return NoContent();

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Cursos. Erro: {ex.Message}");
            }
        }

        [HttpGet("curso/{data}")]
        public async Task<IActionResult> GetAllCursosByDataAsync(string data)
        {
            try
            {
                var parsedDate = DateTime.Parse(data);

                var curso = await _cursoService.GetAllCursosByDataAsync(parsedDate);
                if (curso == null) return NoContent();

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Cursos. Erro: {ex.Message}");
            }
        }

        [HttpGet("cursos/{dataInicio}/{dataTermino}")]
        public async Task<IActionResult> GetAllCursosByDatasAsync(string dataInicio, string dataTermino)
        {
            try
            {
                var parsedDate = DateTime.Parse(dataInicio);
                var parsedDate1 = DateTime.Parse(dataTermino);

                var curso = await _cursoService.GetAllCursosByDatasAsync(parsedDate, parsedDate1);
                if (curso == null) return NoContent();

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Cursos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CursoDto model)
        {
            try
            {
                var curso = await _cursoService.AddCurso(User.GetUserId(), model);
                if (curso == null) return NoContent();

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar Curso. Erro: {ex.Message}");
            }
        }

        [HttpPut("put/{cursoId}")]
        public async Task<IActionResult> Put(int cursoId, CursoDto model)
        {
            try
            {
                var curso = await _cursoService.UpdateCurso(User.GetUserId(), cursoId, model);
                if(curso == null) return NoContent();

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar Curso. Erro: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await _cursoService.GetCursoByIdAsync(id);
                if (evento == null) return NoContent();

                if (await _cursoService.DeleteCurso(User.GetUserId(), id))
                {
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    throw new Exception("Não foi possível deletar este Curso");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar Curso. Erro: {ex.Message}");
            }
        }
    }
}
