using AutoMapper;
using Cursos_API.Application.Dtos;
using Cursos_API.Application.Interfaces;
using Cursos_API.Models;
using Cursos_API.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace Cursos_API.Application
{
    public class CursoService : ICursoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly ICursoPersist _cursoPersist;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public CursoService(IGeralPersist geralPersist, ICursoPersist cursoPersist, IMapper mapper, ILogService logService)
        {
            _geralPersist = geralPersist;
            _cursoPersist = cursoPersist;
            _mapper = mapper;
            _logService = logService;
        }

        public async Task<CursoDto> AddCurso(int userId, CursoDto model)
        {
            try
            {
                var curso = _mapper.Map<Curso>(model);
                curso.UserId = userId;

                var resuDataInicio = await _cursoPersist.GetAllCursosByDataAsync(curso.DataInicio);
                var resuDataTermino = await _cursoPersist.GetAllCursosByDataAsync(curso.DataTermino);

                if (resuDataInicio != null || resuDataTermino != null) throw new Exception("Existe(m) curso(s) planejados(s) dentro do período informado.");

                if (curso.DataInicio >= DateTime.Now &&
                    curso.DataTermino > curso.DataInicio)
                {
                    _geralPersist.Add(curso);

                    if (await _geralPersist.SaveChangesAsync())
                    {
                        LogDto logModel = new LogDto();
                        logModel.DataInclusao = DateTime.Now;
                        await _logService.AddLog(curso.CursoId, logModel);
                    }
                    else
                    {
                        throw new Exception("Não foi possível criar o log");
                    }
                }
                throw new Exception("Data não permitida por já ser uma data passada");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCurso(int userId, int cursoId)
        {
            try
            {
                var curso = await _cursoPersist.GetCursoByIdAsync(cursoId);
                if (curso == null) throw new Exception("Curso para delete não encontrado");
                if (curso.DataInicio < DateTime.Now) throw new Exception(
                    "Este curso não pode ser deletado pois já foi finalizado ou está em andamento.");

                curso.Status = false;

                _geralPersist.Update<Curso>(curso);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var cursoRetorno = await _cursoPersist.GetCursoByIdAsync(curso.CursoId);

                    return true;
                }
                throw new Exception("Não foi possível deletar este curso");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public async Task<CursoDto[]> GetAllCursosAsync()
        {
            try
            {
                var cursos = await _cursoPersist.GetAllCursosAsync();
                if (cursos == null) return null;

                var resultado = _mapper.Map<CursoDto[]>(cursos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CursoDto[]> GetAllCursosByDataAsync(DateTime data)
        {
            try
            {
                var cursos = await _cursoPersist.GetAllCursosByDataAsync(data);
                if (cursos == null) return null;

                var resultado = _mapper.Map<CursoDto[]>(cursos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<CursoDto[]> GetAllCursosByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CursoDto> GetCursoByIdAsync(int cursoId)
        {
            try
            {
                var curso = await _cursoPersist.GetCursoByIdAsync(cursoId);
                if (curso == null) return null;

                var resultado = _mapper.Map<CursoDto>(curso);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CursoDto> UpdateCurso(int userId, int cursoId, CursoDto model)
        {
            try
            {
                var curso = await _cursoPersist.GetCursoByIdAsync(cursoId);
                if (curso == null) return null;

                model.CursoId = cursoId;

                var resuDataInicio = await _cursoPersist.GetAllCursosByDataAsync(model.DataInicio);
                var resuDataTermino = await _cursoPersist.GetAllCursosByDataAsync(model.DataTermino);

                if (resuDataInicio != null || resuDataTermino != null) throw new Exception(
                    "Existe(m) curso(s) planejados(s) dentro do período informado.");

                _mapper.Map(model, curso);

                _geralPersist.Update(curso);

                if (curso.DataInicio >= DateTime.Now &&
                    curso.DataTermino > curso.DataInicio)
                {
                    _geralPersist.Add(curso);

                    if (await _geralPersist.SaveChangesAsync())
                    {
                        LogDto logModel = await _logService.GetLogByCursoIdAsync(curso.CursoId);
                        logModel.DataAtualizacao = DateTime.Now;
                        await _logService.UpdateLog(curso.CursoId, logModel);
                    }
                    else
                    {
                        throw new Exception("Não foi possível atualizar o log");
                    }
                }
                throw new Exception("Data não permitida por já ser uma data passada");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
