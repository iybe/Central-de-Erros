using Api_Central_de_Erros.Models;
using System;

namespace Api_Central_de_Erros.Services
{
    public interface IUserService
    {
        bool CheckEmailExist(string email);
        User Login(string email, string password);
        User Register(User user);
        User Save(User user);
    }
}
