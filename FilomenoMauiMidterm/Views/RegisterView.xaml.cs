namespace FilomenoMauiMidterm.Views;

public partial class RegisterView : ContentPage
{
	bool isPasswordVisible = false;
	const string closedEyeIcon = "password_not_visible.svg";
	const string openEyeIcon = "password_visible.svg";
	public RegisterView()
	{
		InitializeComponent();
	}

	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{
		isPasswordVisible = !isPasswordVisible;
		TogglePasswordButton.Source = isPasswordVisible ? openEyeIcon : closedEyeIcon;
		passwordEntry.IsPassword = !isPasswordVisible;
	}
}