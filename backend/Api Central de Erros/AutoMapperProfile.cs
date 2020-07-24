using AutoMapper;
using Api_Central_de_Erros.DTOs;
using Api_Central_de_Erros.Models;

namespace Codenation.Challenge
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Log, LogInputDTO>().ReverseMap();
            CreateMap<Log, LogOutputDTO>().ReverseMap();
        }
    }
}