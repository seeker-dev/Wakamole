namespace Skyline.Core.DTOs;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public string? NextPageToken { get; set; }
}