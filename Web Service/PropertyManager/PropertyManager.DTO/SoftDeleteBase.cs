using System;
using System.Linq;
using System.Runtime.Serialization;

namespace PropertyManager.DTO
{
    [DataContract]
    public abstract class SoftDeleteBase : UniqueBase
    {
        [DataMember]
        public bool IsDeleted { get; set; }
    }
}
