using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public string Id { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }

        public void cifrar()
        {
            this.Description = Cifrado.Cifrar(this.Description);
            this.Id = Cifrado.Cifrar(this.Id);
        }
        public void decifrar()
        {
            this.Description = Cifrado.Decifrar(this.Description);
            this.Id = Cifrado.Decifrar(this.Id);
        }
    }
}
