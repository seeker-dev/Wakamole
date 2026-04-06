using Microsoft.Extensions.Logging;
using Skyline.Application.Interfaces;
using Skyline.Application.Services;
using Skyline.Core.Interfaces;
using Skyline.Infrastructure.Configuration;
using Skyline.Infrastructure.Data;
using Skyline.Infrastructure.Repositories;
using MudBlazor.Services;

namespace Skyline;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

		builder.Services.AddSingleton(new BlueSkyConfiguration { BaseUrl = "https://bsky.social" });
		builder.Services.AddSingleton<IBlueSkyClient, BlueSkyClient>();
		builder.Services.AddSingleton<IBlueskyService, BlueskyService>();
		builder.Services.AddSingleton<IUserSettingsRepository, FileUserSettingsRepository>();
		builder.Services.AddSingleton<IUserSettingsService, FileUserSettingsService>();
		builder.Services.AddSingleton<IModerationService, ModerationService>();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddMudServices();

		return builder.Build();
	}
}
