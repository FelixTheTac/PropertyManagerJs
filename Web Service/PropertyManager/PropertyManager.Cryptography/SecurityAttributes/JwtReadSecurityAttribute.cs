using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Cryptography.SecurityAttributes
{
    public class JwtReadSecurityAttribute : JwtSecurityAttribute
    {
        public JwtReadSecurityAttribute(string claimIdsValues) : base(claimIdsValues) { }
    }

    public class JwtMyReadSecurityAttribute : JwtReadSecurityAttribute
    {
        public JwtMyReadSecurityAttribute(string claimIdsValues) : base(claimIdsValues) { }
    }
}
