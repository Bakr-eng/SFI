using MongoDB.Bson;
using SFI.Models;
using SFI.Repositories;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SFI.View.Teacher;

public partial class AddNewStudentsPage : ContentPage
{
    private readonly IPersonRepository _personRepo = PersonRepository.Instance;
    private readonly INivåerRepository _nivåRepo = new NivåerRepository();

    private Person _Lärare;
	public AddNewStudentsPage(Person lärare)
	{
		InitializeComponent();
        _Lärare = lärare;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            var nyElev = new Person
            {
                Id = ObjectId.GenerateNewId(),
                Name = StudentName.Text,
                Email = StudentEmail.Text,
                Phone = StudentPhone.Text,
                Lösenord = Password.Text,
                Roll = "Elev",
                KlassId = _Lärare.KlassId,
                LärareId = _Lärare.Id
            };
            await _personRepo.Add(nyElev);

            await DisplayAlert("Klart", "Ny elev har lagts till!", "OK");
            StudentName.Text = string.Empty;
            StudentEmail.Text = string.Empty;
            StudentPhone.Text = string.Empty;
            Password.Text = string.Empty;

            // Skapa nivåer-post för den nya eleven
            var nivåer = new Nivåer
            {
                Id = ObjectId.GenerateNewId(),
                ElevId = nyElev.Id,
                Tala = 0,
                Skriva = 0,
                Läsa = 0,
                Höra = 0,
                UppdateringsDag = DateTime.Now
            };
            await _nivåRepo.Add(nivåer);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fel", $"Ett fel inträffade: {ex.Message}", "OK");
            return;
        }
       
    }
}