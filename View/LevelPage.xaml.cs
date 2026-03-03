using MongoDB.Driver;
using SFI.Models;

namespace SFI.View;

public partial class LevelPage : ContentPage
{
	private Person _elev;
	private Nivåer _nivåer;
    public LevelPage(Person elev, Nivåer nivåer)
	{
		InitializeComponent();
		_elev = elev;
		_nivåer = nivåer;

		_nivåer.UppdateringsDag = _nivåer.UppdateringsDag.ToLocalTime();   // för att spara svensk tid
        BindingContext = _nivåer;

		TalaSlider.Value = _nivåer.Tala;
		SkrivaSlider.Value = _nivåer.Skriva;
		LäsaSlider.Value = _nivåer.Läsa;
		HöraSlider.Value = _nivåer.Höra;
    }

    private async void OnSaveLevelsClicked(object sender, EventArgs e)
    {
		var db = new Data.MongoDb();

		_nivåer.Tala = (int)TalaSlider.Value;
		_nivåer.Skriva = (int)SkrivaSlider.Value;
		_nivåer.Läsa = (int)LäsaSlider.Value;
		_nivåer.Höra = (int)HöraSlider.Value;
		_nivåer.UppdateringsDag = DateTime.Now;

		await db.Nivåer.ReplaceOneAsync(
			Builders<Nivåer>.Filter.Eq(n => n.Id, _nivåer.Id),
			_nivåer
			);

		await DisplayAlert("Sparat", "Nivåerna har sparats.", "OK");
		await Navigation.PopAsync();

    }
}