using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class Door
    {
        public string Code { get; set; } = null!;
        public int? Sequence { get; set; }
        public string? Country { get; set; }
        public string? Description { get; set; }
        public byte? IsClosed { get; set; }
        public string? SeqId { get; set; }

        public virtual Country? CountryNavigation { get; set; }
        public virtual Sequence? SequenceNavigation { get; set; }

        public void cifrar()
        {
            this.Description = Cifrado.Cifrar(this.Description);
            this.Code = Cifrado.Cifrar(this.Code);
            this.Country = Cifrado.Cifrar(this.Country);
            

        }
        public void decifrar()
        {
            this.Description = Cifrado.Decifrar(this.Description);
            this.Code = Cifrado.Decifrar(this.Code);
            this.Country = Cifrado.Decifrar(this.Country);
         
        }
    }
}
