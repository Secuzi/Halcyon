using System.Diagnostics;
using FilomenoMauiMidterm.ViewModels;

namespace FilomenoMauiMidterm.Views;

public partial class LoginView : ContentPage
{
	bool isPasswordVisible = false;
	//const string closedEyeIcon = "&#xe900;";
	//const string openEyeIcon = "&#xe901;";
	const string closedEyeIcon = "password_not_visible.svg";
	const string openEyeIcon = "password_visible.svg";

	public LoginView()
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
	private async void OnRegisterTapped (object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync($"{nameof(RegisterView)}");
	}

	


}