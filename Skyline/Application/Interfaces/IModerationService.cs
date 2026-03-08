using Skyline.Application.DTOs;
using Skyline.Core.DTOs;

namespace Skyline.Application.Interfaces
{
    public interface IModerationService
    {
        public Task RemovePostFromFeedAsync(int postId, PagedResult<PostDto> posts);
    }
}