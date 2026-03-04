using SFI.Models;
using System.Threading.Tasks;

namespace SFI.View.Teacher;

public partial class TeacherPage : ContentPage
{
	private Person _Larare;
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
}