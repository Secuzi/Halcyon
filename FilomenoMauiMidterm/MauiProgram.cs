using System.Text.Json;
using FilomenoMauiMidterm.Services;
using FilomenoMauiMidterm.ViewModels;
using Imagekit.Sdk;
using Microsoft.Extensions.Logging;

namespace FilomenoMauiMidterm
{
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Inter.ttf", "Inter");
                    fonts.AddFont("Inter18ptLight.ttf", "InterLight");
                    fonts.AddFont("Inter24ptMedium.ttf", "InterMedium");
                    fonts.AddFont("Inter24ptSemiBold.ttf", "InterSemiBold");
                    fonts.AddFont("Inter28ptBold.ttf", "InterBold");
                    fonts.AddFont("icomoon.ttf", "Icomoon");
                })
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    handlers.AddHandler<Shell, RoundedFloatingTabbarHandler>();
#endif
                });

            builder.Services.AddSingleton<HttpClient>(new HttpClient() { BaseAddress = new Uri("https://681ebcd2c1c291fa6634fa21.mockapi.io/v1") });
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<JsonSerializerOptions>(
                new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            builder.Services.AddSingleton<ImagekitClient>(_ => new ImagekitClient("public_AuCag7P1Fhba3rO2ED03j3D0RwE=", 
                "private_uThznUMxKh26CbNwdkQ0hdRwVd0=", "https://ik.imagekit.io/2l9fwjwd7/"));
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
