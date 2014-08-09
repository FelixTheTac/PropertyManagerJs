using System;
using System.Linq;
using System.Runtime.Serialization;

namespace PropertyManager.DTO.Models
{
    [DataContract]
    public abstract class User : UniqueBase
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
