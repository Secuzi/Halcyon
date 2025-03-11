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
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
