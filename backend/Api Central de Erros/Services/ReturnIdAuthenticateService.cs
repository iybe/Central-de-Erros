using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api_Central_de_Erros.Services
{
    public class ReturnIdAuthenticateService
    {
        public int Resolve(ClaimsPrincipal User)
        {
            var userId = User.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value)
                .First()
                .ToString();

            return Convert.ToInt32(userId);
        }
    }
}
