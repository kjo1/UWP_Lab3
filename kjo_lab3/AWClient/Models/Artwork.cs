using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AWClient.Models
{
    public class ArtWork
    {
        public int ID { get; set; }

        public string Summary
        {
            get
            {
                return Name + " - " + Finished.ToString();
            }
        }

        public string Name { get; set; }

        public DateTime Finished { get; set; }

        public string Description { get; set; }

        public decimal Value { get; set; }

        public int ArtTypeID { get; set; }

        public virtual ArtType ArtType { get; set; }

        [Timestamp]
        public Byte[] RowVersion { get; set; }
    }
}
