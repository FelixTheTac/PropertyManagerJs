using System;
using System.Linq;
using System.Runtime.Serialization;

namespace PropertyManager.DTO.Models
{
    [DataContract]
    public class Address : SoftDeleteBase
    {
        [DataMember]
        public string AddressLine1 { get; set; }

        [DataMember]
        public string AddressLine2 { get; set; }

        [DataMember]
        public string AddressLine3 { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string StateProvince { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }
    }
}
