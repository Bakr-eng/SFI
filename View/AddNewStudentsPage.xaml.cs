using MongoDB.Bson;
using SFI.Models;
using System.Threading.Tasks;

namespace SFI.View;

public partial class AddNewStudentsPage : ContentPage
{
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
            var db = new Data.MongoDb();
            var nyElev = new Models.Person
            {
                Id = ObjectId.GenerateNewId(),
                Name = StudentName.Text,
                Email = StudentEmail.Text,
                Lösenord = Password.Text,
                Roll = "Elev",
                KlassId = _Lärare.KlassId
            };
            await db.Personer.InsertOneAsync(nyElev);

            await DisplayAlert("Klart", "Ny elev har lagts till!", "OK");
            StudentName.Text = string.Empty;
            StudentEmail.Text = string.Empty;
            Password.Text = string.Empty;


            // Skapa en Nivåer-post för den nya eleven
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
            await db.Nivåer.InsertOneAsync(nivåer);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fel", $"Ett fel inträffade: {ex.Message}", "OK");
            return;
        }

    }
}