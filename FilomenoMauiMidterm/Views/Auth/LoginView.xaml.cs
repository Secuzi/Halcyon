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
	private RegisterViewModel _registerViewModel;
	public LoginView(LoginViewModel loginViewModel, RegisterViewModel registerViewModel)
	{
		InitializeComponent();
		BindingContext = loginViewModel;
		_registerViewModel = registerViewModel;
	}

	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{
		
		isPasswordVisible = !isPasswordVisible;
		TogglePasswordButton.Source = isPasswordVisible ? closedEyeIcon : openEyeIcon;
		passwordEntry.IsPassword = !isPasswordVisible;

	}
	private async void OnRegisterTapped (object sender, TappedEventArgs e)
	{
		//await Shell.Current.GoToAsync($"{nameof(RegisterView)}");
		await Navigation.PushAsync(new RegisterView(_registerViewModel));
	}


	protected override void OnAppearing()
	{
		base.OnAppearing();

	}



}