namespace Links.Api.Storage.Entities;

public class LinkEntity {
    public required string Id { get; set; }
    public required string ShortId { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required string OwnerId { get; set; }

    public required string ForwardingUri { get; set; }
}
