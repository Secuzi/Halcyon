using FilomenoMauiMidterm.ViewModels;
using FilomenoMauiMidterm.Views;
using Microsoft.Maui.Platform;
namespace FilomenoMauiMidterm
{
    public partial class App : Application
    {
        public App(LoginViewModel loginViewModel, RegisterViewModel registerViewModel)
        {
            InitializeComponent();
            

            MainPage = new NavigationPage(new LoginView(loginViewModel, registerViewModel));
#if ANDROID
           Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
			{
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());

            });

           Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("CursorColor", (handler, view) =>
            {
                handler.PlatformView.TextCursorDrawable.SetTint(Colors.Cyan.ToPlatform());

            });
#endif
        }
    }
}
