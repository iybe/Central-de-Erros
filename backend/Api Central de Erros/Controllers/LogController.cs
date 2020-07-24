using Api_Central_de_Erros.DTOs;
using Api_Central_de_Erros.Models;
using Api_Central_de_Erros.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Api_Central_de_Erros.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private ILogService _service;
        private readonly IMapper _mapper;

        public LogController(ILogService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public ActionResult<LogOutputDTO> CreateLog([FromBody] LogInputDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados Incompletos");

            if (model.environment != "Homologação" && model.environment != "Produção" && model.environment != "Dev")
            {
                return BadRequest("Ambiente incorreto");
            }

            if (model.level != "error" && model.level != "warning" && model.level != "debug")
            {
                return BadRequest("Level incorreto");
            }

            var log = _mapper.Map<Log>(model);
            log.createdAt = DateTime.Now;

            try
            {
                var logSave = _service.Save(log, User);

                return Ok(_mapper.Map<LogOutputDTO>(logSave));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("show")]
        [Authorize]
        public ActionResult<List<LogOutputDTO>> ShowLog()
        {
            return Ok(_service.Show(User)
                .Select(x => _mapper.Map<LogOutputDTO>(x))
                .ToList());
        }

        [HttpGet]
        [Route("searchEnvironment/{environment}")]
        [Authorize]
        public ActionResult<List<LogOutputDTO>> EnvironmentLog(string environment)
        {
            if (environment != "Homologação" && environment != "Produção" && environment != "Dev")
            {
                return BadRequest("Ambiente incorreto");
            }

            return Ok(_service.SearchByEnvironment(environment, User)
                .Select(x => _mapper.Map<LogOutputDTO>(x))
                .ToList());

        }

        [HttpGet]
        [Route("orderFrequencyUpward")]
        [Authorize]
        public ActionResult<List<LogOutputDTO>> OrderFrequencyUpward()
        {
            return Ok(_service.OrderByFrequency(true, User)
                .Select(x => _mapper.Map<LogOutputDTO>(x))
                .ToList());

        }

        [HttpGet]
        [Route("orderFrequencyDownward")]
        [Authorize]
        public ActionResult<List<LogOutputDTO>> OrderFrequencyDownward()
        {
            return Ok(_service.OrderByFrequency(false, User)
                .Select(x => _mapper.Map<LogOutputDTO>(x))
                .ToList());

        }

        [HttpGet]
        [Route("searchLevel/{level}")]
        [Authorize]
        public ActionResult<List<LogOutputDTO>> LevelLog(string level)
        {
            if (level != "error" && level != "warning" && level != "debug")
            {
                return BadRequest("Level incorreto");
            }

            return Ok(_service.SearchByLevel(level, User)
                .Select(x => _mapper.Map<LogOutputDTO>(x))
                .ToList());

        }

        [HttpGet]
        [Route("Upfrequency/{logId}")]
        [Authorize]
        public ActionResult<LogOutputDTO> UpFrequencyLog(int logId)
        {
            try
            {
                var log = _service.Frequency(logId, User);

                return Ok(_mapper.Map<LogOutputDTO>(log));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize]
        public ActionResult<dynamic> DeleteLog([FromHeader]int logId)
        {
            try
            {
                _service.Delete(logId, User);

                return Ok(new { message = "Log removido com sucesso" });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("deleteMany")]
        [Authorize]
        public ActionResult<dynamic> DeleteManyLog([FromHeader] string logs)
        {
            try
            {
                var logIds = logs.Split(",").Select(x => int.Parse(x)).ToList();

                _service.DeleteMany(logIds, User);

                return Ok(new { message = "Logs removidos com sucesso" });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
