using Links.Api.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Links.Api.Storage.Repositories;

public class LinkRepository {
    private readonly LinksStorage storage;

    public LinkRepository( LinksStorage storage ) {
        this.storage = storage;
    }

    public async Task<LinkEntity?> GetByShortId( string short_id ) => await this.storage.Links.FirstOrDefaultAsync( link => link.ShortId == short_id );
}
