using System.Diagnostics;
using FilomenoMauiMidterm.Models;
using FilomenoMauiMidterm.ViewModels;
using Syncfusion.Maui.Toolkit.BottomSheet;

namespace FilomenoMauiMidterm.Views.Tabs;

public partial class HomeView : ContentPage
{
    HomeViewModel _homeViewModel;
    CancellationTokenSource _cancellationTokenSource;
	public HomeView(HomeViewModel homeViewModel)
	{
		InitializeComponent();
        _homeViewModel = homeViewModel;
        BindingContext = _homeViewModel;
	}
	private async void HomePage_Clicked(object sender, EventArgs e)
	{
		await DisplayAlert("Alert", "This is HomePAge", "OK");
	}
    private bool isLiked = false;

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _cancellationTokenSource = new CancellationTokenSource();
        try
        {
            await _homeViewModel.LoadDataAsync(_cancellationTokenSource.Token);

        }
        catch (OperationCanceledException)
        {

            Debug.Write("Operation Cancelled");
        }
    }


    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _homeViewModel.UserPosts = [];
        _cancellationTokenSource?.Cancel();
        _homeViewModel.IsDeleteModalEnabled = false;
        // Clean up resources
        PostOptionsBottomSheet.Close();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = null;
    }
    

}