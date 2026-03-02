using MongoDB.Driver;
using SFI.Models;

namespace SFI.View;

public partial class ShowStudentInfoPage : ContentPage
{
	private Person _elev;
    public  ShowStudentInfoPage(Person elev)
    {
        InitializeComponent();
        _elev = elev;

        LoadStudentInfo();

       
    }
    private async void LoadStudentInfo()
    {
        NamnLabel.Text = $"Namn: {_elev.Name}";
        EmailLabel.Text = $"E-post: {_elev.Email}";
        LösenordLabel.Text = $"Lösenord: {_elev.Lösenord}";

        if (_elev.KlassId.HasValue)
        {
            var db = new Data.MongoDb();

            var klass = await db.Klasser
                .Find(k => k.Id == _elev.KlassId.Value)
                .FirstOrDefaultAsync();

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
            var db = new Data.MongoDb();
            await db.Personer.DeleteOneAsync(p => p.Id == _elev.Id);
            await DisplayAlert("Klart", "Eleven har raderats!", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void OnLevelClicked(object sender, EventArgs e)
    {
        var db = new Data.MongoDb();

        var nivå = await db.Nivåer
            .Find(n => n.ElevId == _elev.Id)
            .FirstOrDefaultAsync();
        if (nivå == null)
        {
            await DisplayAlert("Ingen nivå", "Nivåer hittades inte för denna elev.", "OK");
            return;
        }


        await Navigation.PushAsync(new LevelPage(_elev, nivå));
    }
}