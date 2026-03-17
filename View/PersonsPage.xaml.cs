using SFI.Models;
using SFI.Repositories;

namespace SFI.View;

public partial class PersonsPage : ContentPage
{
	private readonly IKlassRepository _klassRepo = new KlassRepository();
	private readonly Person _person;
	public PersonsPage(Person person)
	{
		InitializeComponent();
		_person = person;
		LoadPersonInfo();
	}
	private async Task LoadPersonInfo()
	{
        NamnLabel.Text = $"Namn:   {_person.Name}";
        EmailLabel.Text = $"E-post:   {_person.Email}";
        PhoneLabel.Text = $"Telefonnummer:   {_person.Phone}";
        LösenordLabel.Text = $"Lösenord:   {_person.Lösenord}";

		var klass = await _klassRepo.GetById(_person.KlassId.Value);
        if (klass != null)
        {
            KlassIdLabel.Text = $"Klass: {klass.Name}";
        }
        else
        {
            KlassIdLabel.Text = "Klass: Okänd";
        }
    }
}