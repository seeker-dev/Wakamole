using Microsoft.Extensions.Logging;
using Skyline.Application.Interfaces;
using Skyline.Application.Services;
using Skyline.Core.Interfaces;
using Skyline.Infrastructure.Configuration;
using Skyline.Infrastructure.Data;
using Skyline.Infrastructure.Repositories;

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

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
