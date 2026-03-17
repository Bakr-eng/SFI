using SFI.Models;
using System.Threading.Tasks;

namespace SFI.View.Teacher;

public partial class TeacherPage : ContentPage
{
    private readonly Person _Larare;
    public TeacherPage(Person Larare)
	{
		InitializeComponent();
		_Larare = Larare;

		welcomeLabel.Text = $"Välkommen, {_Larare.Name}!";
    }


    private async void OnManageStudentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageStudentsPage(_Larare));
    }

    private async void OmMessageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SendMessagePage(_Larare));
    }
    private async void OnMyprofileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PersonsPage(_Larare));
    }
    private async void OnWeatherClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WeatherPage());
    }

   
}