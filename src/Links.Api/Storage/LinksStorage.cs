using Links.Api.Options;
using Links.Api.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Links.Api.Storage;

public class LinksStorage : DbContext {
    private readonly StorageOptions options;

    public DbSet<LinkEntity> Links { get; set; }

    public LinksStorage( IOptions<StorageOptions> options ) {
        this.options = options.Value;
    }

    protected override void OnConfiguring( DbContextOptionsBuilder options ) => options.UseSqlite( this.options.ConnectionString );

    protected override void OnModelCreating( ModelBuilder mb ) {
        base.OnModelCreating( mb );

        mb.Entity<LinkEntity>().HasData(
            new LinkEntity { Id = "1", CreatedDate = new DateTime( 2025, 1, 1 ), OwnerId = "1", ShortId = "test", ForwardingUri = "https://google.com" }
        );
    }
}
