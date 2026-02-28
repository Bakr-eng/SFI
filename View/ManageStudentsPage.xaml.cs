using MongoDB.Bson;
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
}