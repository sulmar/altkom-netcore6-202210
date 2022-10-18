using AuthService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace AuthService.Infrastructure
{
    public class FakeTokenService : ITokenService
    {
        public string Create(User user)
        {
            return "ABC";
        }
    }
}
