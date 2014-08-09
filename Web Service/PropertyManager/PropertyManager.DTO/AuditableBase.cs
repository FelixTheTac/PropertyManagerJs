using System;
using System.Linq;
using System.Runtime.Serialization;

namespace PropertyManager.DTO
{
    [DataContract]
    public abstract class AuditableBase : SoftDeleteBase
    {
        [DataMember]
        public DateTime? AddedOn { get; set; }

        [DataMember]
        DateTime? LastModifiedOn { get; set; }

        [DataMember]
        int? LastModifiedById { get; set; }

        [DataMember]
        int? AddedById { get; set; }
    }
}
