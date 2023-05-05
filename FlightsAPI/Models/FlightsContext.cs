using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FlightsAPI.Models
{
    public partial class FlightsContext : DbContext
    {

        public FlightsContext(DbContextOptions<FlightsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airline> Airlines { get; set; } = null!;
        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Door> Doors { get; set; } = null!;
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Journal> Journals { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Sequence> Sequences { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("Airline");

                entity.HasIndex(e => new { e.Sequence, e.Code }, "SeqId")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Country)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Description)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.SeqId)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("seq_ID");

                entity.Property(e => e.Sequence).HasColumnName("sequence");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Airlines)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("FK_Airline_country");

                entity.HasOne(d => d.SequenceNavigation)
                    .WithMany(p => p.Airlines)
                    .HasForeignKey(d => d.Sequence)
                    .HasConstraintName("FK_Airline_seq");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.Pan);

                entity.ToTable("Card");

                entity.Property(e => e.Pan)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("PAN");

                entity.Property(e => e.Franchise)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("franchise");

                entity.Property(e => e.UserName)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK_Card_user");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("Country");

                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.SeqId)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("seq_ID");

                entity.Property(e => e.Sequence).HasColumnName("sequence");

                entity.HasOne(d => d.SequenceNavigation)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.Sequence)
                    .HasConstraintName("FK_Country_Seq");
            });

            modelBuilder.Entity<Door>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Door__357D4CF892C63DB4");

                entity.ToTable("Door");

                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Country)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IsClosed).HasColumnName("isClosed");

                entity.Property(e => e.SeqId)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("seq_ID");

                entity.Property(e => e.Sequence).HasColumnName("sequence");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Doors)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("FK_Door_country");

                entity.HasOne(d => d.SequenceNavigation)
                    .WithMany(p => p.Doors)
                    .HasForeignKey(d => d.Sequence)
                    .HasConstraintName("FK_Door_seq");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("errorLog");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.At)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("at");

                entity.Property(e => e.Detail)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("detail");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("Flight");

                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.Airline)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("airline");

                entity.Property(e => e.DateArrive)
                    .HasColumnType("date")
                    .HasColumnName("dateArrive");

                /*entity.Property(e => e.DateDep)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("dateDep");*/

                entity.Property(e => e.DateDep)
                    .HasColumnType("date")
                    .HasColumnName("dateDep");

                entity.Property(e => e.Origin)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("origin");

                entity.Property(e => e.SeqId)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("seq_ID");

                entity.Property(e => e.Sequence).HasColumnName("sequence");

                entity.Property(e => e.Status)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TimeArrive).HasColumnName("timeArrive");

                entity.Property(e => e.To)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("to");

                entity.HasOne(d => d.AirlineNavigation)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.Airline)
                    .HasConstraintName("FK_Flight_airline");

                entity.HasOne(d => d.SequenceNavigation)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.Sequence)
                    .HasConstraintName("FK_Flight_seq");
            });

            modelBuilder.Entity<Journal>(entity =>
            {
                entity.ToTable("Journal");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Action)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("action");

                entity.Property(e => e.At)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("at");

                entity.Property(e => e.Description)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Detail)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("detail");

                entity.Property(e => e.RecordCode)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("recordCode");

                entity.Property(e => e.UserName)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("userName");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("Purchase");

                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Card)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("card");

                entity.Property(e => e.Date)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("date");

                entity.Property(e => e.SeqId)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("seq_ID");

                entity.Property(e => e.Sequence).HasColumnName("sequence");

                entity.Property(e => e.Ticket)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("ticket");

                entity.HasOne(d => d.CardNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Card)
                    .HasConstraintName("FK_Purchase_card");

                entity.HasOne(d => d.SequenceNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Sequence)
                    .HasConstraintName("FK_Purchase_seq");

                entity.HasOne(d => d.TicketNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Ticket)
                    .HasConstraintName("FK_Purchase_tickert");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.BookingId);

                entity.ToTable("Reservation");

                entity.Property(e => e.BookingId)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.SeqId)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("seq_ID");

                entity.Property(e => e.Sequence).HasColumnName("sequence");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Ticket)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("ticket");

                entity.Property(e => e.UserName)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.HasOne(d => d.SequenceNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Sequence)
                    .HasConstraintName("FK_Reservation_seq");

                entity.HasOne(d => d.TicketNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Ticket)
                    .HasConstraintName("FK_Reservation_ticket");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK_Reservation_user");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Sequence>(entity =>
            {
                entity.HasKey(e => e.Sequence1);

                entity.ToTable("Sequence");

                entity.Property(e => e.Sequence1)
                    .ValueGeneratedNever()
                    .HasColumnName("Sequence");

                entity.Property(e => e.Description)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EndRange).HasColumnName("endRange");

                entity.Property(e => e.HasPrefix).HasColumnName("hasPrefix");

                entity.Property(e => e.HasRange).HasColumnName("hasRange");

                entity.Property(e => e.IniRange).HasColumnName("iniRange");

                entity.Property(e => e.LastValue).HasColumnName("lastValue");

                entity.Property(e => e.Prefix)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("prefix");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("Ticket");

                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Flight)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("flight");

                entity.Property(e => e.SeqId)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("seq_ID");

                entity.Property(e => e.Sequnece).HasColumnName("sequnece");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.SequneceNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.Sequnece)
                    .HasConstraintName("FK_Ticket_Sequence");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK_User");

                entity.Property(e => e.UserName)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.SecAnswer)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("secAnswer");

                entity.Property(e => e.SecQuestion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("secQuestion");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
