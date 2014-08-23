using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using PropertyManager.DTO.Domain;

namespace PropertyManager.WebShared.Interfaces.Controllers
{
    public interface ILoginController
    {
        List<Claim> GetClaims([FromBody]
                              GetClaimsRequest claimsRequest);
    }
}