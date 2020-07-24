using Api_Central_de_Erros.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Api_Central_de_Erros.Services
{
    public interface ILogService
    {
        Log Frequency(int logId, ClaimsPrincipal claimUser);
        void Delete(int logId, ClaimsPrincipal claimUser);
        void DeleteMany(List<int> logIds, ClaimsPrincipal claimUser);
        List<Log> Show(ClaimsPrincipal claimUser);
        List<Log> SearchByEnvironment(string environment, ClaimsPrincipal claimUser);
        List<Log> SearchByLevel(string level, ClaimsPrincipal claimUser);
        List<Log> OrderByFrequency(bool upward, ClaimsPrincipal claimUser);
        Log Save(Log log, ClaimsPrincipal claimUser);
    }
}
