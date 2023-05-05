using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class Journal
    {
        public string? UserName { get; set; }
        public long Id { get; set; }
        public string? RecordCode { get; set; }
        public string? Action { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public byte[]? At { get; set; }


        public void cifrar()
        {
            this.UserName = Cifrado.Cifrar(this.UserName);
            this.RecordCode = Cifrado.Cifrar(this.RecordCode);
            this.UserName = Cifrado.Cifrar(this.UserName);
            this.Description = Cifrado.Cifrar(Description);
            this.Detail = Cifrado.Cifrar(Detail);
        }
        public void decifrar()
        {
            this.UserName = Cifrado.Decifrar(this.UserName);
            this.RecordCode = Cifrado.Decifrar(this.RecordCode);
            this.UserName = Cifrado.Decifrar(this.UserName);
            this.Description = Cifrado.Decifrar(Description);
            this.Detail = Cifrado.Decifrar(Detail);
        }

    }
}
