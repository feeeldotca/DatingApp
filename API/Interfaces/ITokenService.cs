using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    // any service of this interface need to accomplish CreateToken function
    // by user registeration data
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}