using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace PropertyManager.DTO
{
    [DataContract]
    public abstract class UniqueBase
    {
        [Key]
        [DataMember]
        public long Id {get; set;}
    }
}
