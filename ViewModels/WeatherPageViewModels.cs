using Microsoft.Maui.Controls;
using SFI.Models;
using SFI.Repositories;
using SFI.View;
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
            set { city = value; OnPropertyChanged(); }
        }

        private string weatherText;
        public string WeatherText
        {
            get => weatherText;
            set { weatherText = value; OnPropertyChanged(); }
        }

        private string backgroundImage;
        public string BackgroundImage
        {
            get => backgroundImage;
            set { backgroundImage = value; OnPropertyChanged(); }
        }

        public ICommand LoadWeatherCommand { get; }

        public WeatherPageViewModels()
        {
            LoadWeatherCommand = new Command(async () => await LoadWeather());
        }
        public async Task LoadWeatherAuto() // för att hämta vädret på plats man befiner sig
        {
            try
            {
                var location = await Geolocation.GetLocationAsync(
                    new GeolocationRequest(GeolocationAccuracy.Medium)
                );
                if ( location == null )
                {
                    WeatherText = "Kunde inte hämta plats.";
                    return;
                }
                var weather = await WeatherRepository.GetWeatherByCoordinatesAsync(
                      location.Latitude,
                      location.Longitude
                 );
               
                var condition = GetWeatherCondition(weather);
                SetBackground(condition);


                WeatherText =
                    $"Din plats\n" +
                    $"{GetWeatherCondition(weather)}\n" +
                    $"Temp: {weather.temp}°C\n" +
                    $"Vind: {weather.wind_speed} m/s\n" +
                    $"Fuktighet: {weather.humidity}%";
            }
            catch (FeatureNotSupportedException)
            {
                WeatherText = "GPS stöds inte på denna enhet.";
            }
            catch (PermissionException)
            {
                WeatherText = "Appen har inte tillåtelse att använda plats.";
            }
            catch (Exception ex)
            {
                WeatherText = $"Fel: {ex.Message}";
            }
        }
        private async Task LoadWeather()
        {
            if (string.IsNullOrWhiteSpace(City))
            {
                await LoadWeatherAuto();
                return;
            }
            var weather = await WeatherRepository.GetWeatherAsync(City);

            if (weather == null)
            {
                WeatherText = "Kunde inte hämta.";
                return;
            }

            var condition = GetWeatherCondition(weather);
            SetBackground(condition);

            WeatherText =
                $"{City}\n" +
                $"{condition}\n" +
                $"Temp: {weather.temp}°C\n" +
                $"Vind: {weather.wind_speed} m/s\n" +
                $"Fuktighet: {weather.humidity}%";
        }
        private string GetWeatherCondition(Weather weather)
        {
            if (weather.temp <= 0 && weather.cloud_pct > 50)
                return "Snöar / Risk för snö";

            if (weather.cloud_pct > 70 && weather.humidity > 70)
                return "Regnar / Risk för regn";

            if (weather.cloud_pct > 50)
                return "Moln";

            return "Sol";
        }

        private void SetBackground(string condition)
        {
            BackgroundImage = condition switch
            {
                "Snöar / Risk för snö" => "snowfall.png",
                "Regnar / Risk för regn" => "rain.png",
                "Moln" => "cloudy.png",
                _ => "sun.png"
            };
        }



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
