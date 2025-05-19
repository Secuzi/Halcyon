using FilomenoMauiMidterm.ViewModels;
namespace FilomenoMauiMidterm.Views;

public partial class RegisterView : ContentPage
{
	bool isPasswordVisible = false;
	const string openEyeIcon = "password_not_visible.svg";
	const string closedEyeIcon = "password_visible.svg";
	RegisterViewModel _registerViewModel;
	public RegisterView(RegisterViewModel registerViewModel)
	{
		InitializeComponent();
		_registerViewModel = registerViewModel;
		BindingContext = _registerViewModel;
	}

	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{
		isPasswordVisible = !isPasswordVisible;
		TogglePasswordButton.Source = isPasswordVisible ? closedEyeIcon : openEyeIcon;
		passwordEntry.IsPassword = !isPasswordVisible;
	}



	private async void OnLoginTapped(object sender, TappedEventArgs e)
	{
		await Navigation.PopToRootAsync();
		
	}

    private async void Register_Clicked(object sender, EventArgs e)
    {
		bool success = await _registerViewModel.RegisterUser();
		if(success)
		{
            await Navigation.PopToRootAsync();
        }
	}
}