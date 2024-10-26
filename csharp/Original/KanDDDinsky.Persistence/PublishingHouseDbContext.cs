using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using KanDDDinsky.Persistence.Authors;
using KanDDDinsky.Persistence.Books;
using KanDDDinsky.Persistence.Books.Entities;
using KanDDDinsky.Persistence.Core.Outbox;
using KanDDDinsky.Persistence.Languages;
using KanDDDinsky.Persistence.Publishers;
using KanDDDinsky.Persistence.Reviewers;
using KanDDDinsky.Persistence.Translators;

namespace KanDDDinsky.Persistence;

public class KanDDDinskyDbContext(DbContextOptions<KanDDDinskyDbContext> options): DbContext(options)
{
    public DbSet<BookEntity> Books { get; set; } = default!;
    public DbSet<ChapterEntity> BookChapters { get; set; } = default!;
    public DbSet<AuthorEntity> Authors { get; set; } = default!;
    public DbSet<LanguageEntity> Languages { get; set; } = default!;
    public DbSet<PublisherEntity> Publishers { get; set; } = default!;
    public DbSet<ReviewerEntity> Reviewers { get; set; } = default!;
    public DbSet<TranslatorEntity> Translators { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorEntity>()
            .ToTable("Authors");

        modelBuilder.Entity<PublisherEntity>().HasData(
            new PublisherEntity { Id = new Guid("c528d322-17eb-47ba-bccf-6cb61d340f09"), Name = "Readers Digest" });

        modelBuilder.Entity<LanguageEntity>()
            .ToTable("Languages");
        modelBuilder.Entity<PublisherEntity>()
            .ToTable("Publishers");

        modelBuilder.Entity<ReviewerEntity>()
            .ToTable("Reviewers");
        modelBuilder.Entity<TranslatorEntity>()
            .ToTable("Translators");

        modelBuilder.Entity<BookEntity>()
            .ToTable("Books")
            .OwnsMany(b => b.Formats, a =>
            {
                a.ToTable("BookFormats");
                a.WithOwner().HasForeignKey("BookId");
                a.HasKey("FormatType", "BookId");
            })
            .OwnsMany(b => b.Chapters, a =>
            {
                a.ToTable("BookChapters");
                a.WithOwner().HasForeignKey("BookId");
                a.HasKey("Number", "BookId");
                a.Property(e => e.Number)
                    .ValueGeneratedNever();
            })
            .OwnsMany(b => b.Translations, a =>
            {
                a.ToTable("BookTranslations");
                a.WithOwner().HasForeignKey("BookId");

                a.Navigation(d => d.Language);
                a.HasKey("BookId", "LanguageId");
            });

        modelBuilder.Entity<BookEntity>().OwnsOne(
            o => o.CommitteeApproval,
            sa =>
            {
                sa.Property(p => p.Feedback);
                sa.Property(p => p.IsApproved);
            });

        modelBuilder.Entity<OutboxMessageEntity>()
            .ToTable("Outbox")
            .HasKey(p => p.Position);
    }
}

public class KanDDDinskyDbContextFactory: IDesignTimeDbContextFactory<KanDDDinskyDbContext>
{
    public KanDDDinskyDbContext CreateDbContext(params string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<KanDDDinskyDbContext>();

        if (optionsBuilder.IsConfigured)
            return new KanDDDinskyDbContext(optionsBuilder.Options);

        //Called by parameterless ctor Usually Migrations
        var environmentName = Environment.GetEnvironmentVariable("EnvironmentName") ?? "Development";

        var connectionString =
            new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build()
                .GetConnectionString("KanDDDinsky");

        optionsBuilder.UseNpgsql(connectionString);

        return new KanDDDinskyDbContext(optionsBuilder.Options);
    }
}
