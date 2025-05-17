using System.Text.Json;
using FFImageLoading.Maui;
using FilomenoMauiMidterm.Context;
using FilomenoMauiMidterm.Services;
using FilomenoMauiMidterm.ViewModels;
using FilomenoMauiMidterm.Views.Tabs;
using Imagekit.Sdk;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;

namespace FilomenoMauiMidterm
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionToolkit()
                .UseFFImageLoading()
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

            builder.Services.AddSingleton<HttpClient>(new HttpClient() { BaseAddress = new Uri("https://681db1d1f74de1d219b0a4f4.mockapi.io") });
            //builder.Services.AddSingleton<HttpClient>(new HttpClient() { BaseAddress = new Uri("https://681ebcd2c1c291fa6634fa21.mockapi.io/v1") });
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddSingleton<LoggedUser>(new LoggedUser());
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<PostService>();
            builder.Services.AddTransient<HomeView>();

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
