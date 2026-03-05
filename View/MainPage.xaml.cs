using MongoDB.Bson;
using MongoDB.Driver;
using SFI.Models;
using SFI.Repositories;
using SFI.View;
using SFI.View.Student;
using SFI.View.Teacher;
using System.Security.Cryptography;

namespace SFI
{
    
    public partial class MainPage : ContentPage
    {
        private readonly IPersonRepository _personRepo = new PersonRepository();
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLogInClicked(object sender, EventArgs e)
        {

            var person = await _personRepo.Login(email.Text, Password.Text);

            if (person == null)
            {
                await DisplayAlert("Fel", "Felaktigt användarnamn eller lösenord", "OK");
                return;
            }

             if (person.Roll == "Elev")
            {
                await Navigation.PushAsync(new StudentPage(person));
            }
            else if (person.Roll == "Lärare")
            {
                await Navigation.PushAsync(new TeacherPage(person));
            }
            else
            {
                await DisplayAlert("Fel", "Okänd roll", "OK");
            }
            email.Text = string.Empty;
            Password.Text = string.Empty;
        }
    }
}
