using System;
using System.Linq;
using System.Threading.Tasks;
using Api_Central_de_Erros.DTOs;
using Api_Central_de_Erros.Models;
using Api_Central_de_Erros.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Central_de_Erros.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> LoginUser([FromBody] UserDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados Incompletos");

            var userMapped = _mapper.Map<User>(model);
            
            try
            {
                var user = _service.Login(userMapped.email, userMapped.password);
                var token = TokenService.GenerateToken(user);
                return Ok(new { Token = token });
            }
            catch
            {
                return BadRequest("Email ou Senha não conferem");
            }

        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public ActionResult<User> CreateUser([FromBody] UserDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados Incompletos");

            var user = _mapper.Map<User>(model);
            user.createdAt = DateTime.UtcNow;

            try
            {
                var userSave = _service.Register(user);
                userSave.userId = 0;
                userSave.password = "";
                return Ok(userSave);
            }
            catch
            {
                return BadRequest("Email indisponivel");
            }
        }
    }
}
