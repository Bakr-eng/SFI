namespace SFI.View.Components;

public partial class LogoutButton : ContentView
{
	public LogoutButton()
	{
		InitializeComponent();
	}

    private async void OnLogOutClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PopToRootAsync();

        
    }
}