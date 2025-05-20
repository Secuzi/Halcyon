using FilomenoMauiMidterm.ViewModels;

namespace FilomenoMauiMidterm.Views.Tabs;

public partial class PostView : ContentPage
{
	PostViewModel _postViewModel;
	public PostView(PostViewModel postViewModel)
	{
		InitializeComponent();
		_postViewModel = postViewModel;
		BindingContext = _postViewModel;
	}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
		
		_postViewModel.PostDescription = string.Empty;
		_postViewModel.PostImage = string.Empty;

	}
}