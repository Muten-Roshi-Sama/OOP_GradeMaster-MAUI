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
        BindingContext = this;
        DataChangedNotifier.OnDataChanged += UpdateData;

    }
	private void OnAddStudentClicked(object sender, EventArgs e)
	{
        var newStudent = new Student(firstNameEntry.Text, lastNameEntry.Text);
        newStudent.pack(); // Save the new student

        //Update Data
        Student.unpackAll();
        OnPropertyChanged(nameof(StudentList));

		firstNameEntry.Text = string.Empty;
		lastNameEntry.Text =string.Empty;

    }

	private void UpdateData()
	{
		Student.unpackAll();
		OnPropertyChanged(nameof(StudentList));
	}














}


