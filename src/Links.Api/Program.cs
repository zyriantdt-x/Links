using Links.Api.Options;
using Links.Api.Services;
using Links.Api.Storage;
using Links.Api.Storage.Repositories;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// add options
builder.Services.AddOptions<StorageOptions>()
    .Bind( builder.Configuration.GetSection( nameof( StorageOptions ) ) );

// add services
builder.Services.AddDbContext<LinksStorage>();
builder.Services.AddScoped<LinkService>();
builder.Services.AddScoped<LinkRepository>();
builder.Services.AddControllers();

// build the host
WebApplication app = builder.Build();

// migrate the db - this should move to ci/cd
using( IServiceScope scope = app.Services.CreateScope() ) {
    LinksStorage storage = scope.ServiceProvider.GetRequiredService<LinksStorage>();
    storage.Database.Migrate();
}

// configure asp
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();