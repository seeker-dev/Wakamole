using System.Text.Json.Serialization;

namespace Skyline.Infrastructure.Models;

public class Post
{
    public string uri { get; set; } = string.Empty;

    public string cid { get; set; } = string.Empty;

    public Author author { get; set; } = new();

    public PostRecord record { get; set; } = new();

    public PostEmbed? embed { get; set; }
}

public class Author
{
    [JsonPropertyName("did")]
    public string Did { get; set; } = string.Empty;

    [JsonPropertyName("handle")]
    public string Handle { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}

public class PostRecord
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}

public class PostEmbed
{
    [JsonPropertyName("images")]
    public List<EmbedImage>? Images { get; set; }
}

public class EmbedImage
{
    [JsonPropertyName("thumb")]
    public string Thumb { get; set; } = string.Empty;

    [JsonPropertyName("fullsize")]
    public string Fullsize { get; set; } = string.Empty;

    [JsonPropertyName("alt")]
    public string Alt { get; set; } = string.Empty;
}