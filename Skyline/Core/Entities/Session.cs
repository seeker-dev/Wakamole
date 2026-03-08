namespace Skyline.Core.Entities;
public class Session
{
  public required string AccessJwt { get; set; }
  public required string RefreshJwt { get; set; }
  public required string Handle { get; set; }
  public required string Did { get; set; }
  public object DidDoc { get; set; }
  public string Email { get; set; }
  public bool EmailConfirmed { get; set; }
  public bool EmailAuthFactor { get; set; }
  public bool Active { get; set; }
  public string Status { get; set; }
}