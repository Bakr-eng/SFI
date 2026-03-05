using SFI.Models;
using System.Threading.Tasks;

namespace SFI.View.Student;

public partial class StudentPage : ContentPage
{
	private Person _Elev;
    public StudentPage(Person Elev)
	{
		InitializeComponent();
		_Elev = Elev;
    }

    
}