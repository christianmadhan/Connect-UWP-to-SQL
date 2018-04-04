namespace MovieWebApi
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MovieContext : DbContext
    {
        public MovieContext()
            : base("name=MovieContext")
        {
        }

        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Studio> Studio { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .Property(e => e.Title)
                .IsFixedLength();

            modelBuilder.Entity<Studio>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Studio>()
                .Property(e => e.HQCity)
                .IsFixedLength();
        }
    }
}
