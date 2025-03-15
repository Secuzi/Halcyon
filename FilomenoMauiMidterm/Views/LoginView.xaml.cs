using System.Diagnostics;

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
		Debug.WriteLine(TogglePasswordButton.Source);
	}

	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{
		
		TogglePasswordButton.Source = isPasswordVisible ? openEyeIcon : closedEyeIcon;
		isPasswordVisible = !isPasswordVisible;
		passwordEntry.IsPassword = isPasswordVisible;

	}

	
}