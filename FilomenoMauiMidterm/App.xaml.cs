using FilomenoMauiMidterm.Services;
using FilomenoMauiMidterm.ViewModels;
using FilomenoMauiMidterm.Views;
using FilomenoMauiMidterm.Views.Tabs;
using Microsoft.Maui.Platform;
namespace FilomenoMauiMidterm
{
    public partial class App : Application
    {

        public App(LoginViewModel loginViewModel, RegisterViewModel registerViewModel, HomeViewModel homeViewModel, PostViewModel postViewModel)
        {
            InitializeComponent();
            //HomeView(homeViewModel)
            //MainPage = new AppShell();
            //MainPage = new NavigationPage(new LoginView(loginViewModel, registerViewModel));
            //MainPage = new PostView(postViewModel);
            MainPage = new SearchView();
#if ANDROID
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
			{
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());

            });
            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(Editor), (handler, view) =>
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
