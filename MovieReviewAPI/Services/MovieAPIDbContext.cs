using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieReviewAPI.Models
{
    public partial class MovieAPIDbContext : DbContext
    {
        public MovieAPIDbContext()
        {
        }

        public MovieAPIDbContext(DbContextOptions<MovieAPIDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieComment> MovieComment { get; set; }
        public virtual DbSet<MovieRating> MovieRating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data source=database-1.cq9fcz0bnrdr.us-east-1.rds.amazonaws.com,1433;database=AWSMovieReview;User=admin;Password=12345678;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Actor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("smalldatetime");

                entity.Property(e => e.DateReleased).HasColumnType("smalldatetime");

                entity.Property(e => e.Genre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MovieImage)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MovieTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MovieVideo)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MovieComment>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("smalldatetime");

                entity.Property(e => e.MovieTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieComment)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movie_Id");
            });

            modelBuilder.Entity<MovieRating>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("smalldatetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieRating)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieId");
            });
        }
    }
}
