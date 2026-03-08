namespace Skyline.Core.Interfaces
{
    public interface IUserSettingsRepository
    {
        Task<IEnumerable<string>> GetProfileDescriptionMutedWords();
        Task SaveProfileDescriptionMutedWordsAsync(IEnumerable<string> settings);
        Task<IEnumerable<string>> GetProfileNameMutedWords();
        Task SaveProfileNameMutedWordsAsync(IEnumerable<string> settings);
    }
}