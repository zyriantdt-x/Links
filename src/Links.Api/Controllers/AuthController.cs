using Links.Shared.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Links.Api.Controllers;

[Route( "api/[controller]" )]
[ApiController]
public class AuthController : ControllerBase {
    private readonly UserManager<IdentityUser> user_manager;
    private readonly RoleManager<IdentityRole> role_manager;
    private readonly IConfiguration configuration;

    public AuthController(
        UserManager<IdentityUser> user_manager,
        RoleManager<IdentityRole> role_manager,
        IConfiguration configuration ) {
        this.user_manager = user_manager;
        this.role_manager = role_manager;
        this.configuration = configuration;
    }

    [HttpPost]
    [Route( "login" )]
    public async Task<IActionResult> Login( [FromBody] LoginRequest request ) {
        IdentityUser? user = await this.user_manager.FindByNameAsync(request.Username);
        if( user is null )
            return this.Unauthorized();

        bool is_valid_password = await this.user_manager.CheckPasswordAsync(user, request.Password);
        if( !is_valid_password )
            return this.Unauthorized();

        IList<string> roles = await this.user_manager.GetRolesAsync( user );

        List<Claim> claims = [
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];

        foreach( string role in roles )
            claims.Add( new Claim( ClaimTypes.Role, role ) );

        JwtSecurityToken token = this.GetToken(claims);

        return this.Ok( new {
            token = new JwtSecurityTokenHandler().WriteToken( token ),
            expiration = token.ValidTo
        } ); // todo: authokresponse
    }

    [HttpPost]
    [Route( "register" )]
    public async Task<IActionResult> Register( [FromBody] RegisterRequest request ) {
        IdentityUser? existing_user = await this.user_manager.FindByNameAsync( request.Username );
        if( existing_user is not null )
            return this.BadRequest( new RegisterFailResponse() {
                Messages = ["Username is in use"]
            } );

        existing_user = await this.user_manager.FindByEmailAsync( request.Email );
        if( existing_user is not null )
            return this.BadRequest( new RegisterFailResponse() {
                Messages = ["Email is in use"]
            } );

        IdentityUser user = new() {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username
        };

        IdentityResult result = await this.user_manager.CreateAsync(user, request.Password);
        if( !result.Succeeded )
            return this.BadRequest( new RegisterFailResponse() {
                Messages = result.Errors.Select( e => e.Description )
            } );

        return this.Ok();
    }

    private JwtSecurityToken GetToken( List<Claim> claims ) {
        SymmetricSecurityKey key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( this.configuration["Authentication:Secret"]! ) );

        var token = new JwtSecurityToken(
                issuer: this.configuration["Authentication:ValidIssuer"],
                audience: this.configuration["Authentication:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials( key, SecurityAlgorithms.HmacSha256 )
        );

        return token;
    }
}
