using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuthorBook2.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Borrower> Borrowers { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<PopularBook> PopularBooks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("authors_pkey");

            entity.ToTable("authors");

            entity.Property(e => e.AuthorId)
                .ValueGeneratedNever()
                .HasColumnName("author_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Country)
                .HasMaxLength(20)
                .HasColumnName("country");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("books_pkey");

            entity.ToTable("books");

            entity.Property(e => e.BookId)
                .ValueGeneratedNever()
                .HasColumnName("book_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Isbn).HasColumnName("isbn");
            entity.Property(e => e.PublishedYear).HasColumnName("published_year");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Borrower>(entity =>
        {
            entity.HasKey(e => e.BorrowerId).HasName("borrowers_pkey");

            entity.ToTable("borrowers");

            entity.Property(e => e.BorrowerId)
                .ValueGeneratedNever()
                .HasColumnName("borrower_id");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("loans_pkey");

            entity.ToTable("loans");

            entity.Property(e => e.LoanId)
                .ValueGeneratedNever()
                .HasColumnName("loan_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");
            entity.Property(e => e.LoanDate).HasColumnName("loan_date");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");
            entity.Property(e => e.Returned).HasColumnName("returned");

            entity.HasOne(d => d.Book).WithMany(p => p.Loans)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("loans_book_id_fkey");

            entity.HasOne(d => d.Borrower).WithMany(p => p.Loans)
                .HasForeignKey(d => d.BorrowerId)
                .HasConstraintName("loans_borrower_id_fkey");
        });

        modelBuilder.Entity<PopularBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("popular_books");

            entity.Property(e => e.NumberOfLoans).HasColumnName("number_of_loans");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
