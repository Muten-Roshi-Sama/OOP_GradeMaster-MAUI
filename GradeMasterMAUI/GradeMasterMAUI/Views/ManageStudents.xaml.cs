namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;


public partial class ManageStudents : ContentPage
{
	public List<Student> StudentList => Student.GetStudentList();
	public ManageStudents()
	{
		InitializeComponent();
		Student.unpackAll();

	}
}