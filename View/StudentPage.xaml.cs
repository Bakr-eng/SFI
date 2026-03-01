using SFI.Models;

namespace SFI.View;

public partial class StudentPage : ContentPage
{
	private Person _Elev;
    public StudentPage(Person Elev)
	{
		InitializeComponent();
		_Elev = Elev;
		welcomeLabel.Text = $"Välkommen, {_Elev.Name}!";
    }
}