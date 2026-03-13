using SFI.ViewModels;

namespace SFI.View;

public partial class WeatherPage : ContentPage
{
	public WeatherPage()
	{
		InitializeComponent();
		BindingContext = new WeatherPageViewModels();
	}

    public void SetBackground(string condition)
    {
        string image = condition switch
        {
            "Snö ❄️" => "snowday.jpg",
            "Regn 🌧️" => "rainday.jpg",
            "Moln ☁️" => "cloudyday.jpg",
            _ => "sunnyday.jpg"
        };

        this.BackgroundImageSource = image;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is WeatherPageViewModels vm)
        {
            await vm.LoadWeatherAuto();   // Hämta vädret direkt
        }
    }
}