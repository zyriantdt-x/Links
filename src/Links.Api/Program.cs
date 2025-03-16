using Links.Api.Options;
using Links.Api.Services;
using Links.Api.Storage;
using Links.Api.Storage.Repositories;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// options
builder.Services.AddOptions<StorageOptions>()
    .Bind( builder.Configuration.GetSection( nameof( StorageOptions ) ) );

// Add services to the container.

builder.Services.AddDbContext<LinksStorage>();
builder.Services.AddScoped<LinkService>();
builder.Services.AddScoped<LinkRepository>();
builder.Services.AddControllers();

WebApplication app = builder.Build();

using( IServiceScope scope = app.Services.CreateScope() ) {
    LinksStorage storage = scope.ServiceProvider.GetRequiredService<LinksStorage>();
    storage.Database.Migrate();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
