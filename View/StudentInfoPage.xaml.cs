using SFI.Models;

namespace SFI.View;

public partial class StudentInfoPage : ContentPage
{
	private Person _elev;
    public StudentInfoPage(Person elev)
    {
        InitializeComponent();
        _elev = elev;

        NameLabel.Text = $"Namn: {elev.Name}";
        EmailLabel.Text = $"E-post: {elev.Email}";
        KlassIdLabel.Text = elev.KlassId.HasValue ? $"Klass ID: {elev.KlassId.Value}" : "Klass ID: Ingen klass tilldelad";
    }
}