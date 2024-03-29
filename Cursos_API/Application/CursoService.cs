﻿using AutoMapper;
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

                if (curso.QtdAlunos <= 0) throw new Exception("Quantidade de alunos inválida.");

                var resuTitulo = await _cursoPersist.GetCursoByTitulo(curso.Descricao);
                if (resuTitulo.Length > 0) throw new Exception("Já existem cursos com este nome.");

                var resuQuery = await _cursoPersist.GetAllCursosByDatasAsync(curso.DataInicio, curso.DataTermino);
                if (resuQuery.Length > 0) throw new Exception("Existe(m) curso(s) planejados(s) dentro do período informado.");

                if (curso.DataInicio >= DateTime.Now &&
                    curso.DataTermino >= curso.DataInicio)
                {
                    _geralPersist.Add(curso);

                    if (await _geralPersist.SaveChangesAsync())
                    {
                        LogDto logModel = new LogDto();
                        logModel.DataInclusao = DateTime.Now;
                        logModel.UserId = userId;
                        logModel.Status = "Curso criado";
                        var resu = await _logService.AddLog(curso.CursoId, logModel);
                        if (resu)
                        {
                            return model;
                        }
                        else
                        {
                            throw new Exception("Não foi possível registrar o log");
                        }
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
                if (curso.DataInicio <= DateTime.Now) throw new Exception(
                    "Este curso não pode ser deletado pois já foi finalizado ou está em andamento.");

                curso.Status = false;

                _geralPersist.Update<Curso>(curso);

                if (await _geralPersist.SaveChangesAsync())
                {
                    LogDto logModel = await _logService.GetLogByCursoIdAsync(curso.CursoId);
                    logModel.DataAtualizacao = DateTime.Now;
                    logModel.Status = "Curso excluído";
                    var resu = await _logService.UpdateLog(curso.CursoId, logModel);
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

        public async Task<CursoDto[]> GetAllCursosByDatasAsync(DateTime dataInicio, DateTime dataTermino)
        {
            try
            {
                var cursos = await _cursoPersist.GetAllCursosByDatasAsync(dataInicio, dataTermino);
                if (cursos == null) return null;

                var resultado = _mapper.Map<CursoDto[]>(cursos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CursoDto[]> GetAllCursosByUserIdAsync(int userId)
        {
            var curso = await _cursoPersist.GetAllCursosByUserIdAsync(userId);
            if (curso == null) return null;

            var resultado = _mapper.Map<CursoDto[]>(curso);

            return resultado;
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
                model.UserId = curso.UserId;

                    var resuQuery = await _cursoPersist.GetCursosByDatasForUpdateAsync(cursoId, model.DataInicio, model.DataTermino);

                    if (resuQuery.Length > 0) throw new Exception(
                        "Existe(m) curso(s) planejados(s) dentro do período informado.");


                _mapper.Map(model, curso);


                if (curso.DataInicio >= DateTime.Now &&
                    curso.DataTermino >= curso.DataInicio)
                {
                    _geralPersist.Update(curso);

                    if (await _geralPersist.SaveChangesAsync())
                    {
                        LogDto logModel = await _logService.GetLogByCursoIdAsync(curso.CursoId);
                        logModel.DataAtualizacao = DateTime.Now;
                        logModel.Status = "Curso atualizado";
                        var resu = await _logService.UpdateLog(curso.CursoId, logModel);
                        if (resu)
                        {
                            return model;
                        }
                        else
                        {
                            throw new Exception("Não foi possível atualizar o log");
                        }
                    }
                    else
                    {
                        throw new Exception("Não foi possível atualizar o curso");
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
