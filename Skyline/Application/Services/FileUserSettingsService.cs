using Skyline.Application.Interfaces;
using Skyline.Core.Interfaces;

namespace Skyline.Application.Services
{
    public class FileUserSettingsService (IUserSettingsRepository userSettingsRepository) : IUserSettingsService
    {

        public async Task<IEnumerable<string>> GetProfileDescriptionMutedWords()
        {
            return await userSettingsRepository.GetProfileDescriptionMutedWords();
        }

        public async Task<IEnumerable<string>> GetProfileNameMutedWords()
        {
            return await userSettingsRepository.GetProfileNameMutedWords();
        }
    }
}