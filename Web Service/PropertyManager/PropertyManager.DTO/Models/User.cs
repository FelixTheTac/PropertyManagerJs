using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PropertyManager.DTO.Enums;

namespace PropertyManager.DTO.Models
{
    [DataContract]
    public class User : SoftDeleteBase
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public UserTypeEnum UserType { get; set; }


        //navigation
        public virtual IEnumerable<Address> Addresses { get; set; }

        public virtual IEnumerable<Permission> Permissions { get; set; }
    }
}
