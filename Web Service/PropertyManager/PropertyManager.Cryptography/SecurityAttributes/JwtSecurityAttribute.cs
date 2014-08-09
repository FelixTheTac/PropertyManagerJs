using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PropertyManager.Cryptography.SecurityAttributes
{
    public class JwtSecurityAttribute : Attribute
    {

        public List<Dictionary<string, object>> ClaimIdsValues { get; private set; }

        public JwtSecurityAttribute(string claimIdsValues)
        {
            ClaimIdsValues = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(claimIdsValues);
        }
    }
}
