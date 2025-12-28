using CodeFirst.DataAccess.Configurations;
using CodeFirst.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.DataAccess.Context
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Copies> Copies { get; set; }
        public DbSet<Starring> Starring { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<Rentals> Rentals { get; set; }
        public DbSet<Employees> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MoviesConfiguration());
            modelBuilder.ApplyConfiguration(new CopiesConfiguration());
            modelBuilder.ApplyConfiguration(new ActorsConfiguration());
            modelBuilder.ApplyConfiguration(new StarringConfiguration());
            modelBuilder.ApplyConfiguration(new RentalsConfiguration());
            modelBuilder.ApplyConfiguration(new ClientsConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeesConfiguration());

	    modelBuilder.Entity<Movies>(entity =>
            {
                entity.ToTable("MOVIES");
                entity.Property(e => e.MovieId).HasColumnName("MOVIE_ID");
                entity.Property(e => e.Title).HasColumnName("TITLE");
                entity.Property(e => e.Year).HasColumnName("RELEASE_YEAR");
		entity.Property(e => e.AgeRestriction).HasColumnName("AGE_RESTRICTION");
		entity.Property(e => e.Price).HasColumnName("PRICE");
            });
            modelBuilder.Entity<Copies>().ToTable("COPIES");
            modelBuilder.Entity<Actors>(entity =>
            {
                entity.ToTable("ACTORS");
                entity.Property(e => e.ActorId).HasColumnName("ID");
                entity.Property(e => e.Firstname).HasColumnName("FIRSTNAME");
		entity.Property(e => e.Lastname).HasColumnName("LASTNAME");
            });
            modelBuilder.Entity<Starring>().ToTable("STARRING");
            modelBuilder.Entity<Rentals>().ToTable("RENTALS");
            modelBuilder.Entity<Employees>().ToTable("EMPLOYEES");
        }
    }
}
