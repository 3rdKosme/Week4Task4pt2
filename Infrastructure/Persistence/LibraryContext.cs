using Microsoft.EntityFrameworkCore;
using Week4Task4pt2.Domain.Models;

namespace Week4Task4pt2.Infrastructure.Persistence;

public class LibraryContext(DbContextOptions<LibraryContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired().HasMaxLength(200);
            entity.Property(a => a.DateOfBirth).IsRequired();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
            entity.Property(b => b.PublishedYear).IsRequired();
            entity.HasOne(b => b.Author).WithMany(a => a.Books).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.Cascade);
        });

        var authors = new Author[]
        {
            new() { Id = 1, Name = "Leo Tolstoy", DateOfBirth = new DateOnly(1828, 9, 9) },
            new() { Id = 2, Name = "Fyodor Dostoevsky", DateOfBirth = new DateOnly(1821, 11, 11) },
            new() { Id = 3, Name = "Anton Chekhov", DateOfBirth = new DateOnly(1860, 1, 29) },
            new() { Id = 4, Name = "George Orwell", DateOfBirth = new DateOnly(1903, 6, 25) },
            new() { Id = 5, Name = "Jane Austen", DateOfBirth = new DateOnly(1775, 12, 16) }
        };


        var books = new Book[]
        {
            new() { Id = 1, Title = "War and Peace", PublishedYear = 1869, AuthorId = 1 },
            new() { Id = 2, Title = "Anna Karenina", PublishedYear = 1877, AuthorId = 1 },
            new() { Id = 3, Title = "Crime and Punishment", PublishedYear = 1866, AuthorId = 2 },
            new() { Id = 4, Title = "The Brothers Karamazov", PublishedYear = 1880, AuthorId = 2 },
            new() { Id = 5, Title = "The Cherry Orchard", PublishedYear = 1904, AuthorId = 3 },
            new() { Id = 6, Title = "The Seagull", PublishedYear = 1896, AuthorId = 3 },
            new() { Id = 7, Title = "1984", PublishedYear = 1949, AuthorId = 4 },
            new() { Id = 8, Title = "Animal Farm", PublishedYear = 1945, AuthorId = 4 },
            new() { Id = 9, Title = "Pride and Prejudice", PublishedYear = 1813, AuthorId = 5 },
            new() { Id = 10, Title = "Sense and Sensibility", PublishedYear = 1811, AuthorId = 5 }
        };

        modelBuilder.Entity<Author>().HasData(authors);
        modelBuilder.Entity<Book>().HasData(books);

        base.OnModelCreating(modelBuilder);
    }
}
