using SFI.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SFI.ViewModels
{
    class WeatherPageViewModels : INotifyPropertyChanged
    {
        private string city;

        public string City
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged();
            }
        }

        private string weatherText;
        public string WeatherText
        {
            get => weatherText;
            set
            {
                weatherText = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadWeatherCommand { get;}

        public WeatherPageViewModels()
        {
            LoadWeatherCommand = new Command(async () => await LoadWeather());
        }

        private async Task LoadWeather()
        {
            var weather = await WeatherRepository.GetWeatherAsync(City);

            if (weather != null)
            {
                WeatherText =
                    $"{City}\n" +
                    $"Temp: {weather.temp}°C\n" +
                    $"Vind: {weather.wind_speed} m/s\n" +
                    $"Fuktighet: {weather.humidity}%";
            }
            else
            {
                WeatherText = "Kunde inte hämta.";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
