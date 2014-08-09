using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PropertyManager.Cryptography;
using PropertyManager.Cryptography.SecurityAttributes;
using PropertyManager.DTO;
using PropertyManager.DTO.Context;
using PropertyManager.DTO.Domain;
using PropertyManager.WebShared;

namespace PropertyManager.Web.Controllers
{
    public class GenericController : ApiController
    {
        private DBContext _context;
        public GenericController()
        {
            _context = new DBContext();
        }

        [HttpGet]
        [JwtAuthorize()]
        public async Task<IHttpActionResult> GetAll(string typeName)
        {
            try
            {
                //handle security
                Assembly assembly = typeof(GetClaimsRequest).Assembly;
                Type t = assembly.GetType(typeName.Replace("_", "."));
                //->handle read security
                var readSecurityAttribute = t.GetCustomAttribute(typeof(JwtReadSecurityAttribute)) as JwtReadSecurityAttribute;
                var readMySecurityAttribute = t.GetCustomAttribute(typeof(JwtMyReadSecurityAttribute)) as JwtMyReadSecurityAttribute;
                if (!(readSecurityAttribute == null && readMySecurityAttribute == null))
                    if (!(SatisfiesSecurity(readSecurityAttribute) || SatisfiesSecurity(readMySecurityAttribute)))
                        return BadRequest("Security Exception");

                var set = _context.Set(t);
                return Ok((IQueryable<UniqueBase>)set);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        [JwtAuthorize]
        [ResponseType(typeof(UniqueBase))]
        public async Task<IHttpActionResult> GetUniqueBase(string typeName, long id)
        {
            try
            {
                //handle security
                Assembly assembly = typeof(GetClaimsRequest).Assembly;
                Type t = assembly.GetType(typeName.Replace("_", "."));
                //->handle read security
                var readSecurityAttribute = t.GetCustomAttribute(typeof(JwtReadSecurityAttribute)) as JwtReadSecurityAttribute;
                var readMySecurityAttribute = t.GetCustomAttribute(typeof(JwtMyReadSecurityAttribute)) as JwtMyReadSecurityAttribute;
                if (!(readSecurityAttribute == null && readMySecurityAttribute == null))
                    if (!(SatisfiesSecurity(readSecurityAttribute) || SatisfiesSecurity(readMySecurityAttribute)))
                        return BadRequest("Security Exception");

                var set = _context.Set(t);
                var obj = await set.FindAsync(id);
                return Ok(obj);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }

        [HttpPut]
        [JwtAuthorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUniqueBase(string typeName, JObject putObj)
        {
            try
            {
                //handle security
                Assembly assembly = typeof(GetClaimsRequest).Assembly;
                Type t = assembly.GetType(typeName.Replace("_", "."));
                //->handle read security
                var updateSecurityAttribute = t.GetCustomAttribute(typeof(JwtUpdateSecurityAttribute)) as JwtUpdateSecurityAttribute;
                var updateMySecurityAttribute = t.GetCustomAttribute(typeof(JwtMyUpdateSecurityAttribute)) as JwtMyUpdateSecurityAttribute;
                if (!(updateSecurityAttribute == null && updateMySecurityAttribute == null))
                    if (!(SatisfiesSecurity(updateSecurityAttribute) || SatisfiesSecurity(updateMySecurityAttribute)))
                        return BadRequest("Security Exception");



                var obj = putObj.ToObject(t);

                _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPost]
        [JwtAuthorize]
        [ResponseType(typeof(UniqueBase))]
        public async Task<IHttpActionResult> PostUniqueBase(string typeName, JObject postObj)
        {
            try
            {
                //handle security
                Assembly assembly = typeof(GetClaimsRequest).Assembly;
                Type t = assembly.GetType(typeName.Replace("_", "."));
                //->handle read security
                var updateSecurityAttribute = t.GetCustomAttribute(typeof(JwtUpdateSecurityAttribute)) as JwtUpdateSecurityAttribute;
                var updateMySecurityAttribute = t.GetCustomAttribute(typeof(JwtMyUpdateSecurityAttribute)) as JwtMyUpdateSecurityAttribute;
                if (!(updateSecurityAttribute == null && updateMySecurityAttribute == null))
                    if (!(SatisfiesSecurity(updateSecurityAttribute) || SatisfiesSecurity(updateMySecurityAttribute)))
                        return BadRequest("Security Exception");

                var obj = postObj.ToObject(t);
                _context.Set(t).Add(obj);
                await _context.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { typeName = typeName, id = ((UniqueBase)obj).Id }, obj);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            //return CreatedAtRoute("DefaultApi", new { typeName = typeName, id = 0 }, postObj);
        }

        [HttpDelete]
        [JwtAuthorize]
        [ResponseType(typeof(UniqueBase))]
        public async Task<IHttpActionResult> DeleteUniqueBase(string typeName, long id)
        {
            try
            {
                //handle security
                Assembly assembly = typeof(GetClaimsRequest).Assembly;
                Type t = assembly.GetType(typeName.Replace("_", "."));
                //->handle read security
                var deleteSecurityAttribute = t.GetCustomAttribute(typeof(JwtDeleteSecurityAttribute)) as JwtDeleteSecurityAttribute;
                var deleteMySecurityAttribute = t.GetCustomAttribute(typeof(JwtMyDeleteSecurityAttribute)) as JwtMyDeleteSecurityAttribute;
                if (!(deleteSecurityAttribute == null && deleteMySecurityAttribute == null))
                    if (!(SatisfiesSecurity(deleteSecurityAttribute) || SatisfiesSecurity(deleteMySecurityAttribute)))
                        return BadRequest();

                var set = _context.Set(t);
                var obj = await set.FindAsync(id);
                if (obj == null)
                    return NotFound();

                set.Remove(obj);
                await _context.SaveChangesAsync();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }


        }

        #region private methods

        private bool SatisfiesSecurity(JwtSecurityAttribute attribute)
        {
            if (attribute == null)
                return false;
            ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;
            foreach (var dict in attribute.ClaimIdsValues)
            {
                if (JWTService.ValidateClaimsInPrincipal(dict, principal))
                { return true; }
            }
            return false;
        }

        private object DynamicToType(dynamic obj, Type t)
        {
            string ser = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject(ser, t);
        }
        #endregion
    }
}