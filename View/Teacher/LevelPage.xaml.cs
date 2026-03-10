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

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        var slider = (Slider)sender;
		double value = e.NewValue;

		Color color;
		if (value < 50)
		{
			// Röd till Gul
            double t = value / 50;
            color = Color.FromRgb(255, (int)(255 * t), 0);
        }
		else
		{
            // Gul till Grön
            double t = (value - 50) / 50;
            color = Color.FromRgb((int)(255 * (1 - t)), 255, 0);
        }

        slider.MinimumTrackColor = color;
    }
}