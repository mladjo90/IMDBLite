using System;
using IMDBLite.API.DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace IMDBLite.API.DataModels
{
    public partial class dataContext : DbContext
    {
        private IConfiguration Configuration { get; }

        public dataContext()
        {

        }
        public dataContext(DbContextOptions<dataContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Cast> Casts { get; set; }
        public virtual DbSet<CastInMovie> CastInMovies { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Cast>(entity =>
            {
                entity.ToTable("casts");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Gender)
                    .HasColumnType("bit(1)")
                    .HasComment("0 - male; 1 -female");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<CastInMovie>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cast_in_movie");

                entity.HasIndex(e => e.CastId, "FK_cast_in_movie_CastID");

                entity.HasIndex(e => e.MovieId, "FK_cast_in_movie_MovieID");

                entity.Property(e => e.CastId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CastID");

                entity.Property(e => e.MovieId)
                    .HasColumnType("int(11)")
                    .HasColumnName("MovieID");

                entity.HasOne(d => d.Cast)
                    .WithMany()
                    .HasForeignKey(d => d.CastId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cast_in_movie_CastID");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cast_in_movie_MovieID");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.CoverImg).HasMaxLength(255);

                entity.Property(e => e.IsTvshow)
                    .HasColumnName("IsTVShow")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("ratings");

                entity.HasIndex(e => e.MovieId, "FK_ratings_MovieID");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.MovieId)
                    .HasColumnType("int(11)")
                    .HasColumnName("MovieID");

                entity.Property(e => e.RatingStarts).HasPrecision(2, 1);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ratings_MovieID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
