using SFI.Models;
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
                var condition = GetWeatherCondition(weather);

                WeatherText =
                    $"{City}\n" +
                    $"{condition}\n"+
                    $"Temp: {weather.temp}°C\n" +
                    $"Vind: {weather.wind_speed} m/s\n" +
                    $"Fuktighet: {weather.humidity}%";
            }
            else
            {
                WeatherText = "Kunde inte hämta.";
            }
        }
        private string GetWeatherCondition(Weather weather)
        {
            if (weather.temp <= 0 && weather.cloud_pct > 50)
                return "Snöar / Risk för snö ❄️";

            if (weather.cloud_pct > 70 && weather.humidity > 70)
                return "Regn / Risk för regn 🌧️ ";

            if (weather.cloud_pct> 50)
                return "Molnigt ☁️ ";

            return "Klart väder ☀️";
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
