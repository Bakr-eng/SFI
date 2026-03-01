using MongoDB.Bson;
using MongoDB.Driver;
using SFI.Models;
using ZstdSharp.Unsafe;

namespace SFI.View;

public partial class ManageStudentsPage : ContentPage
{
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

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

   
    private async void OnShowStudentClicked(object sender, EventArgs e)
    {
        var db = new Data.MongoDb();

        var students = await db.Personer
            .Find(p => p.KlassId == _Larare.KlassId && p.Roll == "Elev")
            .ToListAsync();

        StudentsList.ItemsSource = students;

        // await Navigation.PushAsync(new ShowStudentsPage(_Larare));
    }

    private async void OnStudentSelected(object sender, SelectionChangedEventArgs e)
    {
        if(e.CurrentSelection.FirstOrDefault() is Person elev)
        {
            await Navigation.PushAsync(new StudentInfoPage(elev));
        }

    }
}