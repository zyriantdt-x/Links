using Links.Api.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Links.Api.Storage;

public class LinksStorage : DbContext {
    public DbSet<LinkEntity> Links { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) {
        if(Directory.Exists("/var/Links.Api"))
            options.UseSqlite($"Data Source=/var/Links.Api/LinksStorage.db");
        if(Directory.Exists("C:\\etc"))
            options.UseSqlite($"Data Source=C:\\etc\\LinksStorage.db");
    }

    protected override void OnModelCreating( ModelBuilder mb ) {
        base.OnModelCreating( mb );

        mb.Entity<LinkEntity>().HasData(
            new LinkEntity { Id = "1", CreatedDate = new DateTime(2025, 1, 1), OwnerId = "1", ShortId = "test", ForwardingUri = "https://google.com" }
        );
    }
}
