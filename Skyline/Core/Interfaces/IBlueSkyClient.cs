using Skyline.Core.Entities;

namespace Skyline.Core.Interfaces
{
    public interface IBlueSkyClient
    {
        Task LoginAsync(string username, string password);
        Task<IEnumerable<Feed>> GetFeedsAsync();
        Task<FeedResponse> GetPostsAsync(string feedId, int limit = 30, string cursor = "");
        Task<Author> GetAuthorAsync(string did);
    }
}