

using FilomenoMauiMidterm.Views;
using FilomenoMauiMidterm.Views.Tabs;

namespace FilomenoMauiMidterm
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
            Routing.RegisterRoute(nameof(PostView), typeof(PostView));


        }
    }
}
