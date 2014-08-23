using System;
using System.Linq;
using System.Runtime.Serialization;

namespace PropertyManager.DTO.Models
{
    [DataContract]
    public class Permission : UniqueBase
    {
        [DataMember]
        public string Description { get; set; }


    }
}
