using Links.Api.Options;
using Links.Api.Services;
using Links.Api.Storage;
using Links.Api.Storage.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// add options
builder.Services.AddOptions<StorageOptions>()
    .Bind( builder.Configuration.GetSection( nameof( StorageOptions ) ) );

// add storage
builder.Services.AddDbContext<LinksStorage>();
builder.Services.AddDbContext<IdentityStorage>();

// add services
builder.Services.AddScoped<LinkService>();
builder.Services.AddScoped<LinkRepository>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityStorage>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication( options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
} ).AddJwtBearer( options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters() {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Authentication:ValidAudience"],
        ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( builder.Configuration["Authentication:Secret"]! ) )
    };
} );
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();