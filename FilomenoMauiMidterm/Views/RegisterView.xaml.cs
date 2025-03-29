using FilomenoMauiMidterm.ViewModels;
namespace FilomenoMauiMidterm.Views;

public partial class RegisterView : ContentPage
{
	bool isPasswordVisible = false;
	const string closedEyeIcon = "password_not_visible.svg";
	const string openEyeIcon = "password_visible.svg";
	public RegisterView()
	{
		InitializeComponent();
		BindingContext = new AuthViewModel(this);
	}

	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{
		isPasswordVisible = !isPasswordVisible;
		TogglePasswordButton.Source = isPasswordVisible ? openEyeIcon : closedEyeIcon;
		passwordEntry.IsPassword = !isPasswordVisible;
	}
	private async void OnLoginTapped(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync($"{nameof(LoginView)}");
		
	}
}