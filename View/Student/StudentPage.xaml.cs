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

		LoadMessages();
    }

	private async void LoadMessages()
	{
		var messages = await _meddelandeRepo.GetMessagesForStudent(_elev.Id, _elev.KlassId.Value);
        MessagesList.ItemsSource = messages;

    }

    
}