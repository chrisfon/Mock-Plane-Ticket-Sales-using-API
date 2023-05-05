using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class Country
    {
        public Country()
        {
            Airlines = new HashSet<Airline>();
            Doors = new HashSet<Door>();
        }

        public string Code { get; set; } = null!;
        public int? Sequence { get; set; }
        public string? Description { get; set; }
        public string? SeqId { get; set; }

        public virtual Sequence? SequenceNavigation { get; set; }
        public virtual ICollection<Airline> Airlines { get; set; }
        public virtual ICollection<Door> Doors { get; set; }


        public void cifrar()
        {
            this.Description = Cifrado.Cifrar(this.Description);
            this.Code = Cifrado.Cifrar(this.Code);
        }
        public void decifrar()
        {
            this.Description = Cifrado.Decifrar(this.Description);
            this.Code = Cifrado.Decifrar(this.Code);
        }

    }
}
