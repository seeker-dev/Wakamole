namespace Skyline.Core.Interfaces
{
    public interface IUserSettingsRepository
    {
        List<string> GetProfileDescriptionMutedWords();
        List<string> GetProfileNameMutedWords();
    }
}