using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AWClient.Models
{
    [DataContract]
    public class ArtWork
    {
        [DataMember]

        public int ID { get; set; }

        //[Display(Name = "Artwork")]
        //public string Summary
        //{
        //    get
        //    {
        //        return Name + " - " + Finished.ToShortDateString();
        //    }
        //}

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime Finished { get; set; }

        [DataMember]
        public string Description { get; set; }
        public decimal Value { get; set; }

        [DataMember]
        public ArtType ArtType { get; set; }

        [DataMember]
        public Byte[] RowVersion { get; set; }
    }
}
