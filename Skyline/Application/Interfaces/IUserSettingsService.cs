namespace Skyline.Application.Interfaces;

public interface IUserSettingsService
{
    List<string> GetProfileDescriptionMutedWords();
    List<string> GetProfileNameMutedWords();
}