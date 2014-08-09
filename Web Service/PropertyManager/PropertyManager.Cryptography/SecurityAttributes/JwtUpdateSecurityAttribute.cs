using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Cryptography.SecurityAttributes
{
    public class JwtUpdateSecurityAttribute : JwtSecurityAttribute
    {
        public JwtUpdateSecurityAttribute(string claimIdsValues) : base(claimIdsValues) { }
    }

    public class JwtMyUpdateSecurityAttribute : JwtUpdateSecurityAttribute
    {
        public JwtMyUpdateSecurityAttribute(string claimIdsValues) : base(claimIdsValues) { }
    }
}
