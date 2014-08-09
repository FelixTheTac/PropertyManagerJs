using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using PropertyManager.Cryptography;
using PropertyManager.Cryptography.SecurityAttributes;
using PropertyManager.Logger;
using Newtonsoft.Json;

namespace PropertyManager.WebShared
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly List<Dictionary<string, object>> _claimIdsValues;


        public JwtAuthorizeAttribute(string claimIdsValues)
        {
            //#if DEBUG
            //    Debugger.Launch();
            //#endif
            _claimIdsValues = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(claimIdsValues);
        }

        public JwtAuthorizeAttribute()
        {
            _claimIdsValues = null;
        }



        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            IEnumerable<string> authValues;
            if (actionContext.Request.Headers.TryGetValues("Authorization", out authValues))
            {
                string token = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();
                if (!string.IsNullOrEmpty(token))
                {
                    var _principal = JWTService.GetClaimsPrincipal(token, ConfigurationManager.AppSettings["SigningCertThumbprint"]);
                    if (_principal.Identity.IsAuthenticated)
                    {
                        if (_claimIdsValues != null)
                        {
                            //authorize with claim ids and values
                            bool isAuthorized = false;
                            foreach (var dict in _claimIdsValues)
                            {
                                if (JWTService.ValidateClaimsInPrincipal(dict, _principal))
                                { isAuthorized = true; break; }
                            }

                            if (!isAuthorized)
                            {
                                HttpContext.Current.Response.AddHeader("Authorization", token);
                                HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
                                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, HttpContext.Current.Response);
                                this.Log().Warn(string.Format("JwtAttribute: ValidateClaimsInPrincipal failed for token: {0}", token));
                                return false;
                            }
                            else
                            {

                                //authorized with claims checking

                                HttpContext.Current.Response.AddHeader("Authorization", token);
                                HttpContext.Current.Response.AddHeader("AuthenticationStatus", "Authorized");
                                HttpContext.Current.User = _principal;
                                this.Log().Debug(string.Format("JwtAttribute: ValidateClaimsInPrincipal successful for token: {0}", token));
                                return true;
                            }

                        }
                        else
                        {

                            //authorized but no claims checking required

                            HttpContext.Current.Response.AddHeader("Authorization", token);
                            HttpContext.Current.Response.AddHeader("AuthenticationStatus", "Authorized");
                            HttpContext.Current.User = _principal;
                            this.Log().Warn(string.Format("JwtAttribute: Authorized but no claims checking required.  Consider using Ignore on Authorize attribute instead"));
                            return true;
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.AddHeader("Authorization", token);
                        HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, HttpContext.Current.Response);
                        this.Log().Warn(string.Format("JwtAttribute: Authorize failed for token: {0}", token));
                        return false;
                    }
                }
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.ExpectationFailed, HttpContext.Current.Response);
                actionContext.Response.ReasonPhrase = "Authorization token was missing.";
                this.Log().Error(string.Format("JwtAttribute: Authorization token was missing."));
                return false;
            }
            else
            {
                return true;
            }




        }

    }
}
