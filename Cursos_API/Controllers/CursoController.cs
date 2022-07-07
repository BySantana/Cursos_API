using Cursos_API.Application.Dtos;
using Cursos_API.Application.Interfaces;
using Cursos_API.Controllers.Extensions;
using Cursos_API.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cursos_API.Controllers
{
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
        [AllowAnonymous]
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
                    $"Erro ao tentar recuperar Posts. Erro: {ex.Message}");
            }
        }

        [HttpGet("curso/{id}")]
        [AllowAnonymous]
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
                    $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        [AllowAnonymous]
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
                    $"Erro ao tentar adicionar curso. Erro: {ex.Message}");
            }
        }

        [HttpPut("put/{cursoId}")]
        [AllowAnonymous]
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
                    $"Erro ao tentar atualizar curso. Erro: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        [AllowAnonymous]
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
                    throw new Exception("Não foi possível deletar este curso");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }
    }
}
