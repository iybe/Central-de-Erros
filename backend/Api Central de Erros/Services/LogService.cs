using Api_Central_de_Erros.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Api_Central_de_Erros.Services
{
    public class LogService : ILogService
    {
        private DatabaseContext _context;
        private ReturnIdAuthenticateService _returnIdAuthenticate;

        public LogService(DatabaseContext context)
        {
            _context = context;
            _returnIdAuthenticate = new ReturnIdAuthenticateService();
        }

        public void Delete(int logId, ClaimsPrincipal claimUser)
        {
            var userId = _returnIdAuthenticate.Resolve(claimUser);
            var search = _context.Logs.Any(x => x.userId == userId && x.logId == logId);

            if(!search)
            {
                throw new Exception();
            }

            var log = _context.Logs
                .Where(x => x.logId == logId)
                .First();

            _context.Logs.Remove(log);
            _context.SaveChanges();
        }

        public void DeleteMany(List<int> logIds, ClaimsPrincipal claimUser)
        {
            foreach(int logId in logIds)
            {
                try
                {
                    Delete(logId, claimUser);
                }
                catch
                {
                    continue;
                }
            }
        }

        public Log Frequency(int logId, ClaimsPrincipal claimUser)
        {
            var userId = _returnIdAuthenticate.Resolve(claimUser);
            var search = _context.Logs.Any(x => x.userId == userId && x.logId == logId);

            if (!search)
            {
                throw new Exception("Log não esta vinculado ao usuario");
            }

            var log = _context.Logs
                .Where(x => x.logId == logId)
                .First();

            log.frequency++;

            var state = log.logId == 0 ? EntityState.Added : EntityState.Modified;
            _context.Entry(log).State = state;
            _context.SaveChanges();
            return log;
        }

        public List<Log> OrderByFrequency(bool upward, ClaimsPrincipal claimUser)
        {
            var userId = _returnIdAuthenticate.Resolve(claimUser);

            if (upward)
            {
                return _context.Logs
                .Where(x => x.userId == userId)
                .OrderBy(x => x.frequency)
                .ToList();
            }
            else
            {
                return _context.Logs
                .Where(x => x.userId == userId)
                .OrderByDescending(x => x.frequency)
                .ToList();
            }
        }

        public Log Save(Log log, ClaimsPrincipal claimUser)
        {
            var userId = _returnIdAuthenticate.Resolve(claimUser);
            log.userId = userId;

            var state = log.logId == 0 ? EntityState.Added : EntityState.Modified;
            _context.Entry(log).State = state;
            _context.SaveChanges();
            return log;
        }

        public List<Log> SearchByEnvironment(string environment, ClaimsPrincipal claimUser)
        {
            var userId = _returnIdAuthenticate.Resolve(claimUser);

            return _context.Logs
                .Where(x => x.userId == userId && x.environment == environment)
                .ToList();
        }

        public List<Log> SearchByLevel(string level, ClaimsPrincipal claimUser)
        {
            var userId = _returnIdAuthenticate.Resolve(claimUser);

            return _context.Logs
                .Where(x => x.userId == userId && x.level == level)
                .ToList();
        }

        public List<Log> Show(ClaimsPrincipal claimUser)
        {
            var userId = _returnIdAuthenticate.Resolve(claimUser);

            return _context.Logs
                .Where(x => x.userId == userId)
                .ToList();
        }
    }
}
