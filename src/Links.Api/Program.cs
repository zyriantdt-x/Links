using Links.Api.Services;
using Links.Api.Storage;
using Links.Api.Storage.Repositories;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<LinksStorage>();
builder.Services.AddScoped<LinkService>();
builder.Services.AddScoped<LinkRepository>();
builder.Services.AddControllers();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
