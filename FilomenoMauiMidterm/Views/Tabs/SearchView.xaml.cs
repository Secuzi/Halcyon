using System.Diagnostics;
using FilomenoMauiMidterm.ViewModels;

namespace FilomenoMauiMidterm.Views.Tabs;

public partial class SearchView : ContentPage
{
	SearchViewModel _searchViewModel;
    CancellationTokenSource _cancellationTokenSource;
    CancellationTokenSource _debounceTokenSource;
    public SearchView(SearchViewModel searchViewModel)
	{
		InitializeComponent();
		_searchViewModel = searchViewModel;
		BindingContext = _searchViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _cancellationTokenSource = new CancellationTokenSource();
        try
        {
            await _searchViewModel.LoadDataAsync(_cancellationTokenSource.Token);

        }
        catch (OperationCanceledException)
        {

            Debug.Write("Operation Cancelled");
        }

    }

    private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        _debounceTokenSource?.Cancel();
        _debounceTokenSource = new CancellationTokenSource();
        try
        {
            await Task.Delay(300, _debounceTokenSource.Token);
            _searchViewModel.FilterUsers(e.NewTextValue);
        }
        catch (TaskCanceledException)
        {
            // Ignore cancellation
        }
    }
}