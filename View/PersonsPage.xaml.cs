using SFI.Models;

namespace SFI.View;

public partial class PersonsPage : ContentPage
{
	private readonly Person _person;
	public PersonsPage(Person person)
	{
		InitializeComponent();
		_person = person;
	}
}