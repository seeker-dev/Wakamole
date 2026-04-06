using System.Text.Json.Serialization;

namespace Skyline.Core.Entities;
public class Session
{
  [JsonPropertyName("accessJwt")]
  public required string AccessJwt { get; set; }
  [JsonPropertyName("refreshJwt")]
  public required string RefreshJwt { get; set; }
  [JsonPropertyName("handle")]
  public required string Handle { get; set; }
  [JsonPropertyName("did")]
  public required string Did { get; set; }
  public object DidDoc { get; set; }
  public string Email { get; set; }
  public bool EmailConfirmed { get; set; }
  public bool EmailAuthFactor { get; set; }
  public bool Active { get; set; }
  public string Status { get; set; }
}