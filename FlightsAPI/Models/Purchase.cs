using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class Purchase
    {
        public string Code { get; set; } = null!;
        public int? Sequence { get; set; }
        public string? Card { get; set; }
        public string? Ticket { get; set; }
        public byte[]? Date { get; set; }
        public string? SeqId { get; set; }

        public string? UserName { get; set; }

        public virtual Card? CardNavigation { get; set; }
        public virtual Sequence? SequenceNavigation { get; set; }
        public virtual Ticket? TicketNavigation { get; set; }

        public void cifrar()
        {
            this.Code = Cifrado.Cifrar(this.Code);
            this.Card = Cifrado.Cifrar(this.Card);
            this.UserName = Cifrado.Cifrar(this.UserName);
            this.Ticket = Cifrado.Cifrar(this.Ticket);


        }
        public void decifrar()
        {
            this.Code = Cifrado.Decifrar(this.Code);
            this.Card = Cifrado.Decifrar(this.Card);
            this.Ticket = Cifrado.Decifrar(this.Ticket);
            this.UserName = Cifrado.Decifrar(this.UserName);

        }
    }
}
