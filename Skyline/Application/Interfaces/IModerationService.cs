using Skyline.Application.DTOs;
using Skyline.Core.DTOs;

namespace Skyline.Application.Interfaces
{
    public interface IModerationService
    {
        public Task<bool> IsProfileNameInViolation(string profileName);
        public Task<bool> IsProfileDescriptionInViolation(string profileDescription);
        public Task MarkPostAsViolationAsync(PostDto post);
    }
}