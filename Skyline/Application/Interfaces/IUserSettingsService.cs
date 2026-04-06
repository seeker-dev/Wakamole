namespace Skyline.Application.Interfaces;

public interface IUserSettingsService
{
    Task<IEnumerable<string>> GetProfileDescriptionMutedWords();
    Task<IEnumerable<string>> GetProfileNameMutedWords();
}