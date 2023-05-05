using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class User
    {
        public User()
        {
            Cards = new HashSet<Card>();
            Reservations = new HashSet<Reservation>();
        }

        public string UserName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string SecQuestion { get; set; } = null!;
        public string SecAnswer { get; set; } = null!;

        public virtual Role RoleNavigation { get; set; } = null!;
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

    
        public void cifrar()
        {
            this.Email = Cifrado.Cifrar(this.Email);
            this.Password = Cifrado.Cifrar(this.Password);
            this.UserName = Cifrado.Cifrar(this.UserName);
            this.SecQuestion = Cifrado.Cifrar(SecQuestion);
            this.SecAnswer = Cifrado.Cifrar(SecAnswer);
            this.Role = Cifrado.Cifrar(Role);
        }
        public void decifrar()
        {
            this.Email = Cifrado.Decifrar(this.Email);
            this.Password = Cifrado.Decifrar(this.Password);
            this.UserName = Cifrado.Decifrar(this.UserName);
            this.SecQuestion = Cifrado.Decifrar(this.SecQuestion);
            this.SecAnswer= Cifrado.Decifrar(this.SecAnswer);
            this.Role = Cifrado.Decifrar(this.Role);
        }
    }
}
