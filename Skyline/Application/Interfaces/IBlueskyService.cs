using Skyline.Application.DTOs;
using Skyline.Core.DTOs;

namespace Skyline.Application.Interfaces
{
    public interface IBlueskyService
    {
        Task LoginAsync(string username, string password);

        Task<IEnumerable<FeedDto>> ListUsersFeedsAsync();

        Task<PagedResult<PostDto>> GetFeedPostsAsync(string feedId, int limit = 30, string cursor = "");

        Task<string> GetProfileDescriptionAsync(string did);
    }
}