using FilomenoMauiMidterm.ViewModels;

namespace FilomenoMauiMidterm.Views;

public partial class ProfileView : ContentPage
{
    ProfileViewModel _profileViewModel;

    public ProfileView(ProfileViewModel profileViewModel)
	{
		InitializeComponent();
        _profileViewModel = profileViewModel;
		BindingContext = _profileViewModel;
		
		
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _profileViewModel.LoadDataAsync();
    }



}