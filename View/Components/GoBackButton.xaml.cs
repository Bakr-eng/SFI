namespace SFI.View.Components;

public partial class GoBackButton : ContentView
{
	public GoBackButton()
	{
		InitializeComponent();
	}

    private async void OnGoBackClicked(object sender, EventArgs e)
    {
		await Application.Current.MainPage.Navigation.PopAsync();
    }
}