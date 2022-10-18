using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain
{
    public interface ITokenService
    {
        string Create(User user);
    }
}
