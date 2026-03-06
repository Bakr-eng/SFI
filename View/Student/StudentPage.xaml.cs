using SFI.Models;
using SFI.Repositories;
using System.Threading.Tasks;

namespace SFI.View.Student;

public partial class StudentPage : ContentPage
{
	private readonly Person _elev;
	private readonly IMeddelandeRepository _meddelandeRepo = new MeddelandeRepository();
    public StudentPage(Person elev)
	{
		InitializeComponent();
		_elev = elev;

    }
    private async void OmMessageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SendMessagePage(_elev));
    }
}