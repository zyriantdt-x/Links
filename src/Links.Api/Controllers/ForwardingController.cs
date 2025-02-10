using Links.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Links.Api.Controllers;
[Route( "api/[controller]" )]
[ApiController]
public class ForwardingController : ControllerBase {
    private readonly LinkService link_service;

    public ForwardingController( LinkService link_service ) {
        this.link_service = link_service;
    }

    [HttpGet("/{short_id}")]
    public async Task<IActionResult> Forward( [FromRoute] string short_id ) {
        Uri? forwarding_uri = await this.link_service.GetForwardingUriByShortId( short_id );

        return forwarding_uri == null ? this.NotFound() : this.RedirectPermanent( forwarding_uri.ToString() ); // converting it just to convert it back??? there must be a reason this is a good idea
    }
}
