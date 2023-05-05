using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class Card
    {
        public Card()
        {
            Purchases = new HashSet<Purchase>();
        }

        public string Pan { get; set; } = null!;
        public string? UserName { get; set; }
        public string? Franchise { get; set; }

        public String? Cvc { get; set; }

        public String? Expiration { get; set; }

        public virtual User? UserNameNavigation { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }

        public void cifrar()
        {
            this.Pan = Cifrado.Cifrar(this.Pan);
            this.UserName = Cifrado.Cifrar(this.UserName);
            this.Franchise = Cifrado.Cifrar(this.Franchise);
            this.Cvc = Cifrado.Cifrar(this.Cvc);
            this.Expiration = Cifrado.Cifrar(this.Expiration);


        }
        public void decifrar()
        {
            this.Pan = Cifrado.Decifrar(this.Pan);
            this.UserName = Cifrado.Decifrar(this.UserName);
            this.Franchise = Cifrado.Decifrar(this.Franchise);
            this.Cvc = Cifrado.Decifrar(this.Cvc);
            this.Expiration = Cifrado.Decifrar(this.Expiration);

        }
    }
}
