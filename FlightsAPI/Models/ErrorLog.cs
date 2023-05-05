using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class ErrorLog
    {
        public long Id { get; set; }
        public string? Detail { get; set; }
        public byte[]? At { get; set; }


        public void cifrar()
        {
            this.Detail = Cifrado.Cifrar(this.Detail);

        }
        public void decifrar()
        {
            this.Detail = Cifrado.Decifrar(this.Detail);

        }
    }

   
}
