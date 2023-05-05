using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class Flight
    {
        public string Code { get; set; } = null!;
        public int? Sequence { get; set; }
        public int? Cost { get; set; }
        public string? Origin { get; set; }
        public string? To { get; set; }
        public string? Airline { get; set; }
        public DateTime? DateDep { get; set; }
        public TimeSpan? TimeDep { get; set; }
        public DateTime? DateArrive { get; set; }
        public TimeSpan? TimeArrive { get; set; }
        public string? Status { get; set; }
        public string? SeqId { get; set; }

        public virtual Airline? AirlineNavigation { get; set; }
        public virtual Sequence? SequenceNavigation { get; set; }


        public void cifrar()
        {
            this.Code = Cifrado.Cifrar(this.Code);
            this.Origin = Cifrado.Cifrar(this.Origin);
            this.To = Cifrado.Cifrar(this.To);
            this.Airline = Cifrado.Cifrar(Airline);
            this.Status = Cifrado.Cifrar(Status);
        }
        public void decifrar()
        {
            this.Code = Cifrado.Decifrar(this.Code);
            this.Origin = Cifrado.Decifrar(this.Origin);
            this.To = Cifrado.Decifrar(this.To);
            this.Airline = Cifrado.Decifrar(this.Airline);
            this.Status = Cifrado.Decifrar(this.Status);
        }
    }
}
