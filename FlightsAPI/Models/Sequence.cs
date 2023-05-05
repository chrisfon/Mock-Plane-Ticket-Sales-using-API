using System;
using System.Collections.Generic;

namespace FlightsAPI.Models
{
    public partial class Sequence
    {
        public Sequence()
        {
            Airlines = new HashSet<Airline>();
            Countries = new HashSet<Country>();
            Doors = new HashSet<Door>();
            Flights = new HashSet<Flight>();
            Purchases = new HashSet<Purchase>();
            Reservations = new HashSet<Reservation>();
            Tickets = new HashSet<Ticket>();
        }

        public int Sequence1 { get; set; }
        public string Description { get; set; } = null!;
        public byte HasPrefix { get; set; }
        public string? Prefix { get; set; }
        public byte HasRange { get; set; }
        public int? IniRange { get; set; }
        public int? EndRange { get; set; }
        public int? LastValue { get; set; }

        public virtual ICollection<Airline> Airlines { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Door> Doors { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
