using System.Text.Json.Serialization;

namespace Skyline.Infrastructure.Models;
public class FeedResponse
{
    [JsonPropertyName("feed")]
    public List<FeedItem> Feed { get; set; } = new();

    [JsonPropertyName("cursor")]
    public string? Cursor { get; set; }
}

public class FeedItem
{
    [JsonPropertyName("post")]
    public Post Post { get; set; } = new();
}