namespace FilomenoMauiMidterm.Views.Tabs;

public partial class HomeView : ContentPage
{
	public HomeView()
	{
		InitializeComponent();
	}
	private async void HomePage_Clicked(object sender, EventArgs e)
	{
		await DisplayAlert("Alert", "This is HomePAge", "OK");
	}
    private bool isLiked = false;

    private async void OnLikeTapped(object sender, EventArgs e)
    {
        isLiked = !isLiked;

        // Change image source
        LikeImage.Source = isLiked ? "likefilled_icon.png" : "likeunfilled_icon.png";
        // Animate with a "pop" effect
        await LikeImage.ScaleTo(1.3, 100, Easing.CubicOut);  // Slight zoom in
        await LikeImage.ScaleTo(1.0, 100, Easing.CubicIn);   // Back to normal
        LikeImage.Opacity = 0.5;
        await LikeImage.FadeTo(1.0, 100);
    }

}