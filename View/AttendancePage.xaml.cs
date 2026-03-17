using Microsoft.Maui.Controls.Shapes;
using MongoDB.Bson;
using SFI.Models;
using SFI.Repositories;

namespace SFI.View;

public partial class AttendancePage : ContentPage
{
    private readonly IAttendanceRepository _attendanceRepo = new AttendanceRepository();
	private readonly Person _elev;
    private readonly Person _person;
    private int _currentYear = DateTime.Now.Year;
    public AttendancePage(Person elev, Person perosn)
	{
		InitializeComponent();
		_elev = elev;
        _person = perosn;

        YearLabel.Text = _currentYear.ToString();
        LoadYear(_currentYear);
    }

    private async void LoadYear(int year)
    {
        try
        {
            var attendanceList = await _attendanceRepo.GetByStudentId(_elev.Id);

            var attendanceDict = attendanceList
                .GroupBy(a => a.Datum.Date)
                .ToDictionary(g => g.Key, g => g.First().Status);

            // Fill månader
            FillMonth(JanDays, 1, year, attendanceDict);
            FillMonth(FebDays, 2, year, attendanceDict);
            FillMonth(MarDays, 3, year, attendanceDict);
            FillMonth(AprDays, 4, year, attendanceDict);
            FillMonth(MajDays, 5, year, attendanceDict);
            FillMonth(JunDays, 6, year, attendanceDict);
            FillMonth(JulDays, 7, year, attendanceDict);
            FillMonth(AugDays, 8, year, attendanceDict);
            FillMonth(SepDays, 9, year, attendanceDict);
            FillMonth(OktDays, 10, year, attendanceDict);
            FillMonth(NovDays, 11, year, attendanceDict);
            FillMonth(DecDays, 12, year, attendanceDict);
        }
        catch (NullReferenceException)
        {
            await DisplayAlert("Fel", "Kunde inte läsa närvarodata. Något saknas.", "OK");
        }
        catch (FormatException)
        {
            await DisplayAlert("Fel", "Felaktigt datumformat i databasen.", "OK");
        }
        catch(Exception ex)
        {
            await DisplayAlert("Fel", $"Ett oväntat fel inträffade: {ex.Message}", "OK");
        }
    }
    private void FillMonth(VerticalStackLayout layout, int month, int year, Dictionary<DateTime, int> attendanceDict)
    {
        try
        {
            layout.Children.Clear();

            int days = DateTime.DaysInMonth(year, month);

            for (int day = 1; day <= days; day++)
            {
                DateTime date = new DateTime(year, month, day);

                int status;

                if (attendanceDict.ContainsKey(date))
                {
                    status = attendanceDict[date];
                }
                else if (date == DateTime.Today)
                {
                    // Dagar för idag blir automatiskt närvarande
                    status = 2;
                }
                else
                {
                    status = -1;
                }

                var border = new Border
                {
                    WidthRequest = 28,
                    HeightRequest = 28,
                    StrokeThickness = 1,
                    Stroke = Colors.Black,
                    BackgroundColor = GetStatusColor(status),
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = new CornerRadius(4)
                    },
                    BindingContext = date,
                    Margin = new Thickness(0, 2),
                    Content = new Label
                    {
                        Text = day.ToString(),
                        FontSize = 12,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Colors.Black
                    }
                };
                var tap = new TapGestureRecognizer();
                tap.Tapped += OnDayTapped;
                border.GestureRecognizers.Add(tap);

                layout.Children.Add(border);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            DisplayAlert("Fel", "Datumet ligger utanför giltigt intervall.", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Fel", $"Ett oväntat fel inträffade: {ex.Message}", "OK");
        }
    }
    private async void OnDayTapped(object sender, TappedEventArgs e)
    {
        try
        {
            var border = (Border)sender;
            var date = ((DateTime)border.BindingContext).Date;

            string choice = await DisplayActionSheet(
                $"{date:yyyy-MM-dd}",
                "Avbryt",
                null,
                "Närvarande",
                "Frånvarande",
                "Sjuk"
            );

            int status = choice switch
            {
                "Närvarande" => 2,
                "Frånvarande" => 0,
                "Sjuk" => 1,
                _ => -1
            };

            if (_person.Roll != "Lärare")
            {
                await DisplayAlert("Åtkomst nekad", "Endast lärare kan ändra närvaro.", "OK");
                return;
            }
            if (status == -1)
                return;

            var existing = await _attendanceRepo.GetByDate(_elev.Id, date);

            if (existing == null)
            {
                await _attendanceRepo.Add(new Attendance
                {
                    StudentId = _elev.Id,
                    Datum = date,
                    Status = status
                });
            }
            else
            {
                existing.Status = status;
                await _attendanceRepo.Update(existing);
            }

            border.BackgroundColor = GetStatusColor(status);

        }

        catch (NullReferenceException)
        {
            await DisplayAlert("Fel", "Kunde inte läsa datumet. Försök igen.", "OK");
        }
        catch (FormatException)
        {
            await DisplayAlert("Fel", "Felaktigt datumformat.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fel", $"Ett oväntat fel inträffade: {ex.Message}", "OK");
        }
        }
    private Color GetStatusColor(int status)
    {
        return status switch
        {
            0 => Colors.Red, // Frånvaro
            1 => Colors.Orange,// Sjuk
            2 => Colors.Green, // Närvaro
            _ => Colors.LightGray // Ingen data
        };
    }
    private void OnPrevYearClicked(object sender, EventArgs e)
    {
        _currentYear--;
        YearLabel.Text = _currentYear.ToString();
        LoadYear(_currentYear);
    }
    private void OnNextYearClicked(object sender, EventArgs e)
    {
        _currentYear++;
        YearLabel.Text = _currentYear.ToString();
        LoadYear(_currentYear);
    }
}