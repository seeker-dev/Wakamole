using System.Text.RegularExpressions;
using Skyline.Application.DTOs;
using Skyline.Application.Interfaces;
using Skyline.Core.DTOs;

namespace Skyline.Application.Services
{
    public class ModerationService : IModerationService
    {
        private readonly IUserSettingsService _userSettingsService;

        public ModerationService(IUserSettingsService userSettingsService)
        {
            _userSettingsService = userSettingsService;
        }

        public async Task<bool> IsProfileDescriptionInViolation(string profileDescription)
        {
            var mutedWords = await _userSettingsService.GetProfileDescriptionMutedWords();
            foreach (var mutedWord in mutedWords)
            {
                if (Regex.IsMatch(profileDescription, $@"\b{Regex.Escape(mutedWord)}\b", RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> IsProfileNameInViolation(string profileName)
        {
            var mutedWords = await _userSettingsService.GetProfileNameMutedWords();
            foreach (var mutedWord in mutedWords)
            {
                if (Regex.IsMatch(profileName, $@"\b{Regex.Escape(mutedWord)}\b", RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task MarkPostAsViolationAsync(PostDto post)
        {
            post.IsInViolation = true;
        }
    }
}