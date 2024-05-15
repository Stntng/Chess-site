using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;
using static MychessProj.Models.User;

namespace MychessProj.Models
{
    public partial class mychessContext : DbContext
    {
        public mychessContext()
        {
        }

        public mychessContext(DbContextOptions<mychessContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Meeting> Meetings { get; set; } = null!;
        public virtual DbSet<Registration> Registrations { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

		static mychessContext()
		{
			NpgsqlConnection.GlobalTypeMapper.MapEnum<rank>();
			NpgsqlConnection.GlobalTypeMapper.MapEnum<roles>();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Database=mychess;Username=postgres;Password=1234567Asd;Persist Security Info=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("rank", new[] { "no_cat", "third_jun_cat", "second_jun_cat", "first_jun_cat", "third_cat", "second_cat", "first_cat", "k_m_s" })
                .HasPostgresEnum("roles", new[] { "judge", "player", "mod" });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityCode)
                    .HasName("city_pkey");

                entity.ToTable("city");

                entity.Property(e => e.CityCode)
                    .ValueGeneratedNever()
                    .HasColumnName("city_code");

                entity.Property(e => e.NameCity)
                    .HasMaxLength(30)
                    .HasColumnName("name_city");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.IdLocation)
                    .HasName("location_pkey");

                entity.ToTable("location");

                entity.Property(e => e.IdLocation)
                    .ValueGeneratedNever()
                    .HasColumnName("id_location");

                entity.Property(e => e.Addres)
                    .HasMaxLength(50)
                    .HasColumnName("addres");

                entity.Property(e => e.CityCode).HasColumnName("city_code");

                entity.Property(e => e.NumberOfTables).HasColumnName("number_of_tables");

                entity.HasOne(d => d.CityCodeNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("location_city_code_fkey");
            });

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.HasKey(e => e.IdMeet)
                    .HasName("meeting_pkey");

                entity.ToTable("meeting");

                entity.Property(e => e.IdMeet).HasColumnName("id_meet");

                entity.Property(e => e.DateTime).HasColumnName("date_time");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.IdTourn).HasColumnName("id_tourn");

                entity.Property(e => e.JudgeId).HasColumnName("judge_id");

                entity.Property(e => e.Result)
                    .HasMaxLength(30)
                    .HasColumnName("result");

                entity.Property(e => e.User1).HasColumnName("user_1");

                entity.Property(e => e.User2).HasColumnName("user_2");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Meetings)
                    .HasForeignKey(d => d.IdLocation)
                    .HasConstraintName("meeting_id_location_fkey");
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdTourn })
                    .HasName("pk_1");

                entity.ToTable("registration");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IdTourn).HasColumnName("id_tourn");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.IdTournNavigation)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.IdTourn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_13");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_12");
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.IdTourn)
                    .HasName("tournament_pkey");

                entity.ToTable("tournament");

                entity.Property(e => e.IdTourn).HasColumnName("id_tourn");

                entity.Property(e => e.DateTourn).HasColumnName("date_tourn");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.JudgeId).HasColumnName("judge_id");

                entity.Property(e => e.NameTourn)
                    .HasMaxLength(50)
                    .HasColumnName("name_tourn");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.IdLocation)
                    .HasConstraintName("tournament_id_location_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("User_pkey");

                entity.ToTable("User");

                entity.Property(e => e.IdUser)
                    .ValueGeneratedNever()
                    .HasColumnName("id_user");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.CityCode).HasColumnName("city_code");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .HasColumnName("last_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .HasColumnName("password");

                entity.Property(e => e.SecondName)
                    .HasMaxLength(20)
                    .HasColumnName("second_name");

                entity.HasOne(d => d.CityCodeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CityCode)
                    .HasConstraintName("User_city_code_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
