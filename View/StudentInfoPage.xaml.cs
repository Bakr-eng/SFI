using MongoDB.Driver;
using SFI.Models;

namespace SFI.View;

public partial class StudentInfoPage : ContentPage
{
	private Person _elev;
    public  StudentInfoPage(Person elev)
    {
        InitializeComponent();
        _elev = elev;

        LoadStudentInfo();

       
    }
    private async void LoadStudentInfo()
    {
        NameLabel.Text = $"Namn: {_elev.Name}";
        EmailLabel.Text = $"E-post: {_elev.Email}";

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

}