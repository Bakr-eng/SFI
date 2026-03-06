using SFI.ViewModels;

namespace SFI.View;

public partial class WeatherPage : ContentPage
{
	public WeatherPage()
	{
		InitializeComponent();
		BindingContext = new WeatherPageViewModels();
	}
}