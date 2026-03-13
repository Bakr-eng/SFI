using SFI.ViewModels;

namespace SFI.View;

public partial class WeatherPage : ContentPage
{
	public WeatherPage()
	{
		InitializeComponent();
		BindingContext = new WeatherPageViewModels();
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