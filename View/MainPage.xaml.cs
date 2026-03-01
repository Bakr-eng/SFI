using MongoDB.Bson;
using MongoDB.Driver;
using SFI.Models;
using SFI.View;
using System.Security.Cryptography;

namespace SFI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLogInClicked(object sender, EventArgs e)
        {
            var db = new Data.MongoDb();

            var person = await db.Personer.Find(p => p.Name == Name.Text)
                .FirstOrDefaultAsync();

            if (person == null || person.Lösenord != Password.Text)
            {
                await DisplayAlert("Fel", "Felaktigt användarnamn eller lösenord", "OK");
                return;
            }
            else if (person.Roll == "Elev")
            {
                await Navigation.PushAsync(new StudentPage(person));
                Name.Text = string.Empty;
                Password.Text = string.Empty;
            }
            else if (person.Roll == "Lärare")
            {

                await Navigation.PushAsync(new TeacherPage(person));
                Name.Text = string.Empty;
                Password.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Fel", "Okänd roll", "OK");
            }




        }
    }
    
}
