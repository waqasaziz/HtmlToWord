namespace Domain.Data
{
    using Microsoft.EntityFrameworkCore;

    public class HtmlToWordDbContext : DbContext
    {
        public HtmlToWordDbContext(DbContextOptions options) : base(options) { }

        public DbSet<WordDictionary> WordDictionary { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordDictionary>()
                         .HasKey(x => x.Id);

            modelBuilder.Entity<WordDictionary>()
                        .Property(x => x.Id)
                        .HasColumnType("char(64)")
                        .IsRequired();

            modelBuilder.Entity<WordDictionary>()
                       .Property(x => x.Salt)
                       .HasColumnType("char(64)")
                       .IsRequired();

            modelBuilder.Entity<WordDictionary>()
                     .Property(x => x.Word)
                     .HasColumnType("nvarchar(344)")
                     .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
