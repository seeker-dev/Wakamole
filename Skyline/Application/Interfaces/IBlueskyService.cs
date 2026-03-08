using System.Threading.Tasks;
using Skyline.Application.DTOs;
using Skyline.Core.DTOs;

namespace Skyline.Application.Interfaces
{
    public interface IBlueSkyService
    {
        Task LoginAsync();

        Task<IEnumerable<FeedDto>> ListUsersFeedsAsync();

        Task<PagedResult<PostDto>> GetFeedPostsAsync();

        Task<string> GetProfileDescriptionAsync();
    }
}