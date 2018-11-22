using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamKontrolUnit.BackEnd.Interface
{
    public interface IJwtTokenService
    {
        string BuildToken(string Email);
    }
}
