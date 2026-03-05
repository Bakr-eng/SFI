using MongoDB.Driver;
using SFI.Models;
using SFI.Repositories;

namespace SFI.View.Teacher;

public partial class LevelPage : ContentPage
{
	private readonly INivåerRepository _nivåRepo = new NivåerRepository();
	private readonly Nivåer _nivåer;
    public LevelPage( Nivåer nivåer)
	{
		InitializeComponent();
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

		_nivåer.Tala = (int)TalaSlider.Value;
		_nivåer.Skriva = (int)SkrivaSlider.Value;
		_nivåer.Läsa = (int)LäsaSlider.Value;
		_nivåer.Höra = (int)HöraSlider.Value;
		_nivåer.UppdateringsDag = DateTime.Now;

		await _nivåRepo.Update(_nivåer);

        await DisplayAlert("Sparat", "Nivåerna har sparats.", "OK");
		await Navigation.PopAsync();

    }
}