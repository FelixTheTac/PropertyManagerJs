using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PropertyManager.DTO.Models
{
    [DataContract]
    public class Property : SoftDeleteBase
    {
        public Property()
        {
            GeoLocation = new double[] { 0, 0 };
        }

        [DataMember]
        public string FriendlyName { get; set; }

        //foreign keys
        [DataMember]
        public long AddressId { get; set; }

        [DataMember]
        public double[] GeoLocation { get; set; } //latitude and longitude (double,double)

        //navigation properties
        public virtual Address Location { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
