using MongoDB.Bson;
using MongoDB.Driver;
using SFI.Models;
using SFI.Repositories;
using ZstdSharp.Unsafe;

namespace SFI.View;

public partial class ManageStudentsPage : ContentPage
{
    private readonly IPersonRepository _personRepo = new PersonRepository();
    protected override async void OnAppearing() // Laddar eleverna varje gång sidan visas
    {
        base.OnAppearing(); 
        await LoadStudents();
    }

    private async Task LoadStudents() 
    {
       var students = await _personRepo.GetStudentsByClass(_Larare.KlassId.Value); // Hämtar eleverna i lärarens klass

        StudentsList.ItemsSource = students;


        if (students.Count == 0)
        {
            await DisplayAlert("Information", "Inga elever hittades i din klass.", "OK");
        }
    }



    private Person _Larare;
    public ManageStudentsPage(Person lärare)
	{
		InitializeComponent();
		_Larare = lärare;
    }

    private async void OnAddStudentClicked(object sender, EventArgs e)
    {
		
		await Navigation.PushAsync(new AddNewStudentsPage(_Larare)); // Skickar läraren objektet för att spara klassId
    }

   

    private async void OnStudentSelected(object sender, SelectionChangedEventArgs e)
    {
        if(e.CurrentSelection.FirstOrDefault() is Person elev)
        {
            await Navigation.PushAsync(new ShowStudentInfoPage(elev));
        }
       

    }

   
}