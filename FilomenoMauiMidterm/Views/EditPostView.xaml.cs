using FilomenoMauiMidterm.ViewModels;

namespace FilomenoMauiMidterm.Views;

public partial class EditPostView : ContentPage
{
	public EditPostView(EditPostViewModel editPostViewModel)
	{
		InitializeComponent();
		BindingContext = editPostViewModel;
	}
}