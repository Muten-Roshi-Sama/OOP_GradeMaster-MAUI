namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;
using System.ComponentModel;
using System.Diagnostics;
using Activity = Models.Activity;


public partial class ManageStudents : ContentPage, INotifyPropertyChanged
{
	public List<Student> StudentList => Student.GetStudentList(); //CANNOT be static
	public ManageStudents()
	{
		InitializeComponent();
		Student.UnpackAll();
        BindingContext = this;
        DataChangedNotifier.OnDataChanged += UpdateData;

    }
	private void OnAddStudentClicked(object sender, EventArgs e)
	{
        var newStudent = new Student(firstnameEntry.Text, lastnameEntry.Text);
        newStudent.Pack(); // Save the new student

        //Update Data
        Student.UnpackAll();
        OnPropertyChanged(nameof(StudentList));

		firstnameEntry.Text = string.Empty;
		lastnameEntry.Text =string.Empty;

    }

	private void UpdateData()
	{
		Student.UnpackAll();
		OnPropertyChanged(nameof(StudentList));
	}


}


