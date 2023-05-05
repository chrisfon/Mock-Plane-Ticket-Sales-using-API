using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class Reservation
    {
        public string BookingId { get; set; } = null!;
        public int? Sequence { get; set; }
        public string? UserName { get; set; }
        public string? Ticket { get; set; }
        public string? Status { get; set; }
        public string? SeqId { get; set; }

        public virtual Sequence? SequenceNavigation { get; set; }
        public virtual Ticket? TicketNavigation { get; set; }
        public virtual User? UserNameNavigation { get; set; }

        public void cifrar()
        {
            this.BookingId = Cifrado.Cifrar(this.BookingId);
            this.UserName = Cifrado.Cifrar(this.UserName);
            this.Ticket = Cifrado.Cifrar(this.Ticket);


        }
        public void decifrar()
        {
            this.BookingId = Cifrado.Decifrar(this.BookingId);
            this.UserName = Cifrado.Decifrar(this.UserName);
            this.Ticket = Cifrado.Decifrar(this.Ticket);

        }
    }
}
