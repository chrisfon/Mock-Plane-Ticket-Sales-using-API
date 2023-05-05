using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Purchases = new HashSet<Purchase>();
            Reservations = new HashSet<Reservation>();
        }

        public string Code { get; set; } = null!;
        public int? Sequnece { get; set; }
        public string? Flight { get; set; }
        public int? Cost { get; set; }
        public string? Status { get; set; }
        public string? SeqId { get; set; }

        public virtual Sequence? SequneceNavigation { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public void cifrar()
        {
            this.Code = Cifrado.Cifrar(this.Code);
            this.Flight = Cifrado.Cifrar(this.Flight);


        }
        public void decifrar()
        {
            this.Code = Cifrado.Decifrar(this.Code);
            this.Flight = Cifrado.Decifrar(this.Flight);

        }
    }
}
