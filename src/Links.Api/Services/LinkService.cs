using Links.Api.Storage.Entities;
using Links.Api.Storage.Repositories;

namespace Links.Api.Services;

public class LinkService {
    private readonly LinkRepository repo;

    public LinkService( LinkRepository repo ) {
        this.repo = repo;
    }

    public async Task<Uri?> GetForwardingUriByShortId/*ugly*/( string short_id ) { 
        LinkEntity? entity = await this.repo.GetByShortId( short_id );
        return entity == null ? null : new Uri( entity.ForwardingUri );
    }
}
