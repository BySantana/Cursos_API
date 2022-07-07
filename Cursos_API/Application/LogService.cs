using AutoMapper;
using Cursos_API.Application.Dtos;
using Cursos_API.Application.Interfaces;
using Cursos_API.Models;
using Cursos_API.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace Cursos_API.Application
{
    public class LogService : ILogService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly ILogPersist _logPersist;
        private readonly IMapper _mapper;

        public LogService(IGeralPersist geralPersist, ILogPersist eventoPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _logPersist = eventoPersist;
            _mapper = mapper;
        }

        public async Task<bool> AddLog(int cursoId, LogDto model)
        {
            try
            {
                var log = _mapper.Map<Log>(model);
                log.CursoId = cursoId;
                
                _geralPersist.Add<Log>(log);

                if(await _geralPersist.SaveChangesAsync())
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<LogDto[]> GetAllLogsAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<LogDto> GetLogByCursoIdAsync(int cursoId)
        {
            try
            {
                var logs = await _logPersist.GetLogByCursoIdAsync(cursoId);
                if (logs == null) return null;

                var resultado = _mapper.Map<LogDto>(logs);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<LogDto> GetLogByIdAsync(int logId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateLog(int cursoId, LogDto model)
        {
            try
            {
                var log = await _logPersist.GetLogByCursoIdAsync(cursoId);
                if (log == null) return false;

                model.Id = log.Id;

                _mapper.Map(model, log);

                _geralPersist.Update<Log>(log);

                if (await _geralPersist.SaveChangesAsync())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
