using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using PropertyManager.Cryptography;
using PropertyManager.DTO.Context;
using PropertyManager.DTO.Domain;
using PropertyManager.Logger;

namespace PropertyManager.Web.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public List<Claim> GetClaims([FromBody]GetClaimsRequest claimsRequest)
        {
            List<Claim> claims = new List<Claim>();
            try
            {
                string encPswd = PasswordCrypt.Encrypt(claimsRequest.Password);
                using (var context = new DBContext())
                {
                    var matchingUser = (from user in context.Users
                                        where user.UserName.Equals(claimsRequest.UserName, StringComparison.InvariantCultureIgnoreCase)
                                               && user.Password.Equals(encPswd)
                                               && user.IsActive
                                               && !user.IsDeleted
                                        select user).FirstOrDefault();

                    if (matchingUser != null)
                    {
                        claims.Add(new Claim("Id", Convert.ToString(matchingUser.Id)));
                        claims.Add(new Claim("UserName", matchingUser.UserName));
                        claims.Add(new Claim("FirstName", matchingUser.FirstName));
                        claims.Add(new Claim("LastName", matchingUser.LastName));
                        claims.Add(new Claim("MiddleName", matchingUser.MiddleName));

                        var userPerms = from user in context.Users
                                        from perm in user.Permissions
                                        where user.Id == matchingUser.Id
                                        select perm;

                        foreach (var perm in userPerms)
                            claims.Add(new Claim(perm.Id + "", Convert.ToString(true)));

                        //create token from claims and create
                        string token = JWTService.CreateToken(claims, null, ConfigurationManager.AppSettings["CertThumprint"]);
                        if (!string.IsNullOrEmpty(token))
                        {
                            HttpContext.Current.Response.AddHeader("Authorization", token);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                this.Log().Error("GetClaims caused exception", ex);
            }
            return claims;
        }
    }
}
