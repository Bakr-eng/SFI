using MongoDB.Bson;
using SFI.Models;
using SFI.Repositories;
using System.Threading.Tasks;

namespace SFI.View;

public partial class SendMessagePage : ContentPage
{
	private readonly IMeddelandeRepository _meddelandeRepo = new MeddelandeRepository();
	private readonly Person _perosn;
    public SendMessagePage(Person perosn)
	{
		InitializeComponent();
		_perosn = perosn;
		SetupUI();

	}
	private void SetupUI()
	{
		if(_perosn.Roll == "Lärare")
		{
            RecipientPicker.IsVisible = true;
            RecipientLabel.Text = "Välj mottagare:";
            LoadRecipientsForTeacher(); 
        }
		else if (_perosn.Roll == "Elev")
		{
            RecipientPicker.IsVisible = false;
            RecipientLabel.Text = "Meddelande till din lärare";
        }
	}
	private async void LoadRecipientsForTeacher()
	{
        var elevRepo = new PersonRepository();
        var elever = await elevRepo.GetStudentsByClass(_perosn.KlassId.Value);

        var list = new List<RecipientModels>();

        list.Add( new RecipientModels
        {
            Id = _perosn.KlassId.Value,
            Name = "Hela klassen",
            Typ = "klass"
        });

        foreach (var elev in elever)
        {
            list.Add(new RecipientModels
            {
                Id = elev.Id,
                Name = elev.Name,
                Typ = "Elev"
            });
        }

        RecipientPicker.ItemsSource = list;

    }
    private async void OnSendClicked(object sender, EventArgs e)
    {
        var meddelande = new Meddelande
        {
            AvsändareId = _perosn.Id,
            Text = MessageEntry.Text,
            Tid = DateTime.Now
        };

		if (_perosn.Roll == "Lärare")
		{
            var selected = RecipientPicker.SelectedItem as RecipientModels;
            if (selected == null)
            {
                await DisplayAlert("Fel", "Välj en mottagare.", "OK");
                return;
            }

            meddelande.MotagareTyp = selected.Typ;  // "klass" eller "elev"
            meddelande.MottagareId = selected.Id;
        }
        else if (_perosn.Roll == "Elev")
        {
            // Elev skickar alltid till sin lärare
            meddelande.MotagareTyp = "Lärare";
            meddelande.MottagareId = _perosn.LärareId;
        }

        await _meddelandeRepo.Add(meddelande);

		await DisplayAlert("Skickat", "Meddelande har skickats!", "ok");
		await Navigation.PopAsync();
    }

    private async void ONSeeMessagesClicked(object sender, EventArgs e)
    {
        List<Meddelande> messages;
        if (_perosn.Roll == "Elev")
        {
            messages = await _meddelandeRepo.GetMessagesForStudent(_perosn.Id, _perosn.KlassId.Value);
           // MessagesList.ItemsSource = messages;
        }
        else
        {
             messages = await _meddelandeRepo.GetMessagesForTeacher(_perosn.Id, _perosn.KlassId.Value);
          //  MessagesList.ItemsSource = messages;
        }

        var perosnRepo = new PersonRepository();
        foreach (var m in messages)
        {
            var avsändare = await perosnRepo.GetById(m.AvsändareId);
            m.AvsändareNamn = avsändare?.Name ?? "Okänd";
        }

        MessagesList.ItemsSource = messages;

    }
}