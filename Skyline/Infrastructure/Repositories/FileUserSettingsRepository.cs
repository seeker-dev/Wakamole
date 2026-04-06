using Skyline.Core.Interfaces;
using System.Text.Json;

namespace Skyline.Infrastructure.Repositories;

public class FileUserSettingsRepository : IUserSettingsRepository
{
    private static readonly string _settingsFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Settings"));
    private readonly string _mutedDescriptionWordsFilePath = Path.Combine(_settingsFolder, "muted_description_words.json");
    private readonly string _mutedProfileWordsFilePath = Path.Combine(_settingsFolder, "muted_profile_words.json");

    public async Task<IEnumerable<string>> GetProfileDescriptionMutedWords()
    {
        if (!File.Exists(_mutedDescriptionWordsFilePath))
        {
            return new List<string>(); // Return default settings if file doesn't exist
        }

        var json = await File.ReadAllTextAsync(_mutedDescriptionWordsFilePath);
        return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
    }

    public async Task SaveProfileDescriptionMutedWordsAsync(IEnumerable<string> settings)
    {
        if (!File.Exists(_mutedDescriptionWordsFilePath))
        {
            File.Create(_mutedDescriptionWordsFilePath).Dispose(); // Create the file if it doesn't exist
        }

        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_mutedDescriptionWordsFilePath, json);
    }

    public async Task<IEnumerable<string>> GetProfileNameMutedWords()
    {
        if (!File.Exists(_mutedProfileWordsFilePath))
        {
            return new List<string>(); // Return default settings if file doesn't exist
        }

        var json = await File.ReadAllTextAsync(_mutedProfileWordsFilePath);
        return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
    }

    public async Task SaveProfileNameMutedWordsAsync(IEnumerable<string> settings)
    {
        if (!File.Exists(_mutedProfileWordsFilePath))
        {
            File.Create(_mutedProfileWordsFilePath).Dispose(); // Create the file if it doesn't exist
        }

        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_mutedProfileWordsFilePath, json);
    }
}