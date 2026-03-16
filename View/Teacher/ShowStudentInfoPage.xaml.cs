using MongoDB.Driver;
using SFI.Models;
using SFI.Repositories;
using SFI.View.Teacher;
using System.Threading.Tasks;

namespace SFI.View.Teacher;

public partial class ShowStudentInfoPage : ContentPage
{
    private readonly IKlassRepository _klassRepo = new KlassRepository();
    private readonly IPersonRepository _personRepo = PersonRepository.Instance;
    private readonly INivåerRepository _nivåRepo = new NivåerRepository();
    private readonly IAttendanceRepository _attendanceRepo = new AttendanceRepository();
    private  Person _elev;
    private Person _larare;
    public  ShowStudentInfoPage(Person elev, Person larare)
    {
        InitializeComponent();
        _elev = elev;
        _larare = larare;
        LoadStudentInfo();
    }
    private async Task LoadStudentInfo()
    {
        NamnLabel.Text = $"Namn:   {_elev.Name}";
        EmailLabel.Text = $"E-post:   {_elev.Email}";
        PhoneLabel.Text = $"Telefonnummer:   {_elev.Phone}";
        LösenordLabel.Text = $"Lösenord:   {_elev.Lösenord}";

        if (_elev.KlassId.HasValue)
        {

           var klass = await _klassRepo.GetById(_elev.KlassId.Value);

            if (klass != null)
            {
                KlassIdLabel.Text = $"Klass: {klass.Name}";
            }
            else
            {
                KlassIdLabel.Text = "Klass: Okänd";
            }
        }
        else
        {
            KlassIdLabel.Text = "Klass: Ingen klass tilldelad";
        }
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if(await DisplayAlert("Radera", "Är du säker på att du vill radera denna elev?", "Ja", "Nej"))
        {
            _elev.KlassId = null;  // spara eleven i MongoDb när lärare ta bortar en elev
            await _personRepo.Update(_elev);
           // await _personRepo.Delete(_elev.Id);   
            await DisplayAlert("Klart", "Eleven har raderats!", "OK");
            await Navigation.PopAsync();
        }
    }
    private async void OnLevelClicked(object sender, EventArgs e)
    {
        try
        {

            var nivå = await _nivåRepo.GetByElevId(_elev.Id); 

            if (nivå == null)
            {
                await DisplayAlert("Ingen nivå", "Nivåer hittades inte för denna elev.", "OK");
                return;
            }

            await Navigation.PushAsync(new LevelPage( nivå)); 

        }
        catch (Exception ex)
        {
            await DisplayAlert("Fel", $"Ett fel inträffade: {ex.Message}", "OK");
        }
    }

    private void OnChangePasswordClicked(object sender, EventArgs e)
    {
         PasswordChangePanel.IsVisible = true;
    }

    private async void OnSavePasswordClicked(object sender, EventArgs e)
    {
        if (NewPasswordEntry.Text != RepeatPasswordEntry.Text)
        {
            await DisplayAlert("Fel", "Lösenorden matchar inte.", "OK");
            return;
        }
        else if (NewPasswordEntry.Text.Length < 8)
        {
            await DisplayAlert("Fel", "Lösenordet måste vara minst 8 tecken.", "OK");
            return;
        }

        _elev.Lösenord = NewPasswordEntry.Text;
        await _personRepo.Update(_elev);
        await DisplayAlert("Klart", "Lösenordet har ändrats", "OK");
        
        PasswordChangePanel.IsVisible = false;

        NewPasswordEntry.Text = "";
        RepeatPasswordEntry.Text = "";

        await LoadStudentInfo(); // för att visa direkt ändringar

    }

    private async void OnAttendanceClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AttendancePage(_elev, _larare));
    }


}