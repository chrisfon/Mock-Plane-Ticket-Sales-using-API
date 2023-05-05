using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class Airline
    {
        public Airline()
        {
            Flights = new HashSet<Flight>();
        }

        public string Code { get; set; } = null!;
        public int? Sequence { get; set; }
        public string? Country { get; set; }
        public string? Description { get; set; }
        public string? SeqId { get; set; }

        public virtual Country? CountryNavigation { get; set; }
        public virtual Sequence? SequenceNavigation { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }

        public void cifrar()
        {
            this.Code = Cifrado.Cifrar(this.Code);
            this.Description = Cifrado.Cifrar(this.Description);
            this.Country = Cifrado.Cifrar(this.Country);
        }
        public void decifrar()
        {
            this.Code = Cifrado.Decifrar(this.Code);
            this.Description = Cifrado.Decifrar(this.Description);
            this.Country = Cifrado.Decifrar(this.Country);

        }

    }
}
