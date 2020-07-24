using Api_Central_de_Erros.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Api_Central_de_Erros.Services
{
    public class UserService : IUserService
    {
        private DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public bool CheckEmailExist(string email)
        {
            return _context.Users.Any(x => x.email == email);
        }

        public User Login(string email, string password)
        {
            try
            {
                return _context.Users
                .Where(x => x.email == email && x.password == password)
                .First();
            }
            catch
            {
                throw new Exception();
            }
            

        }

        public User Register(User user)
        {
            if (!CheckEmailExist(user.email))
            {
                return Save(user);
            }
            throw new Exception();
        }

        public User Save(User user)
        {
            var state = user.userId == 0 ? EntityState.Added : EntityState.Modified;
            _context.Entry(user).State = state;
            _context.SaveChanges();
            return user;
        }
    }
}
