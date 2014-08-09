using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security.Tokens;

namespace PropertyManager.Cryptography
{
    public class JWTService
    {

        private static string _asskey = "{4c2fc7b8-3b45-4e51-8a8d-7cf04359b895}{db31b97a-1461-45f9-80b5-0af4f25b7705}{c7f4a6c5-397a-4d8a-874a-9dc24d16b165}";
        private static string _validIssuer = "PropertyManager";
        private static string _requestingUri = "http://PropertyManager";
        /// <summary>
        /// CreateToken takes the requestingUri, claims, lifetime, and the certThumprint and creates a signed token
        /// </summary>
        /// <param name="requestingUri">the website or application unique identifier in Uri form</param>
        /// <param name="claims">the claims found for the user</param>
        /// <param name="lifetime">the lifetime of the token.  Tokens can expire with this feature</param>
        /// <param name="certThumbprint">the thumprint of the certificate used to sign the token</param>
        /// <returns>the signed token as a string</returns>
        public static string CreateToken(List<Claim> claims, Lifetime lifetime, string certThumbprint)
        {
            SigningCredentials creds = null;
            if (!String.IsNullOrEmpty(certThumbprint))
            {
                //get X509Certificate
                var certificate = GetCertificateFromStore(certThumbprint);
                creds = new X509SigningCredentials(certificate);
            }
            else
            {
                var assKey = GetBytes(_asskey);
                creds = new SigningCredentials(new InMemorySymmetricSecurityKey(assKey), "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", "http://www.w3.org/2001/04/xmlenc#sha256");
            }

            //create token with claims
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                TokenIssuerName = _validIssuer,
                AppliesToAddress = _requestingUri,
                Lifetime = lifetime,
                SigningCredentials = creds
            };

            tokenHandler.RequireExpirationTime = false;
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// GetClaimsPrincipal takes the requestingUri, token, and certThumprint of signing cert and returns the ClaimsPrincipal
        /// </summary>
        /// <param name="requestingUri">the site which should have requested the token</param>
        /// <param name="token">the token</param>
        /// <param name="certThumbprint">the thumbprint for the certificate which created the token.</param>
        /// <returns>the ClaimsPrinciple containing the claims within the token</returns>
        public static ClaimsPrincipal GetClaimsPrincipal(string token, string certThumbprint)
        {
            SecurityToken secToken = null;
            if (!string.IsNullOrEmpty(certThumbprint))
            {
                //get X509Certificate
                var certificate = GetCertificateFromStore(certThumbprint);

                //get X509SecurityToken
                secToken = new X509SecurityToken(certificate);
            }
            else
            {
                var assKey = GetBytes(_asskey);
                secToken = new BinarySecretSecurityToken(assKey);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            //create validation parameters
            var validationParameters = new TokenValidationParameters
            {
                AllowedAudience = _requestingUri,
                SigningToken = secToken,
                ValidateIssuer = true,
                ValidIssuer = _validIssuer,
                AudienceUriMode = System.IdentityModel.Selectors.AudienceUriMode.Always
            };
            //validate token
            tokenHandler.RequireExpirationTime = false;
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(new JwtSecurityToken(token), validationParameters);
            return claimsPrincipal;
        }

        public static bool ValidateClaimsInPrincipal(Dictionary<string, object> claimIdValue, ClaimsPrincipal principal)
        {
            foreach (string key in claimIdValue.Keys)
            {
                if (claimIdValue[key] == null)
                {
                    if (principal.FindFirst(key) == null)
                        return false;
                }
                else
                {
                    if (principal.FindFirst(c => c.Type == key && c.Value == Convert.ToString(claimIdValue[key])) == null)
                        return false;
                }
            }
            return true;
        }



        private static X509Certificate2 GetCertificateFromStore(string certThumbprint)
        {

            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreLocation.LocalMachine);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;
                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead: 
                // currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, true);
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindByThumbprint, certThumbprint, false);
                if (signingCert.Count == 0)
                    return null;
                // Return the first certificate in the collection, has the right name and is current. 
                return signingCert[0];
            }
            finally
            {
                store.Close();
            }

        }

        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static dynamic GetUser(ClaimsPrincipal principal)
        {
            IDictionary<string, object> user = new ExpandoObject() as IDictionary<string, object>;

            foreach (var claim in principal.Claims)
            {
                user[claim.Type] = claim.Value;
            }

            return user;
        }
    }
}
