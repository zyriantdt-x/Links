using Links.Api.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Links.Api.Storage;

public class IdentityStorage : IdentityDbContext<IdentityUser> {
    private readonly StorageOptions options;

    public IdentityStorage( IOptions<StorageOptions> options ) {
        this.options = options.Value;
    }

    protected override void OnConfiguring( DbContextOptionsBuilder options ) => options.UseSqlite( this.options.ConnectionString );
}
