using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PropertyManager.Cryptography.SecurityAttributes
{
    public class JwtDeleteSecurityAttribute : JwtSecurityAttribute
    {
        public JwtDeleteSecurityAttribute(string claimIdsValues) : base(claimIdsValues) { }
    }

    public class JwtMyDeleteSecurityAttribute : JwtDeleteSecurityAttribute
    {
        public JwtMyDeleteSecurityAttribute(string claimIdsValues) : base(claimIdsValues) { }
    }
}
