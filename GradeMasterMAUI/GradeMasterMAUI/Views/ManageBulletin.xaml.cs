namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;
using System.ComponentModel;

using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Activity = Models.Activity;

public partial class ManageBulletin : ContentPage
{
    private Student _selectedStudent;
    public List<Student> StudentList => Student.GetStudentList(); //CANNOT be static
    public List<Eval> SelectedStudentEvals => SelectedStudent?.GetStudentEvalList();

    

    public ManageBulletin()
	{
		InitializeComponent();
        Student.UnpackAll();
        _selectedStudent?.UpdateStudentEvalList(); //if NOT null,Update
        BindingContext = this;
        DataChangedNotifier.OnDataChanged += UpdateData;
    }

    private void UpdateData()
    {
        Activity.UnpackAll();
        Student.UnpackAll();
        Eval.UnpackAll();
        _selectedStudent?.UpdateStudentEvalList(); //if NOT null,Update
        OnPropertyChanged(nameof(StudentList));
    }


    private void OnStudentPickerSelectionChanged(object sender, EventArgs eventArgs)
    {
        var studentPicker = sender as Picker;
        if (studentPicker != null && studentPicker.SelectedItem is Student selectedStudent)
        {
            _selectedStudent = selectedStudent; // Assuming selectedProf is a class-level variable
            //Debug.WriteLine($"SelectedProf is {selectedStudent.DisplayName}");
            _selectedStudent?.UpdateStudentEvalList(); //if NOT null,Update
            OnPropertyChanged(nameof(SelectedStudentEvals));
        }
        //Debug.WriteLine($"SelectedProf is {selectedStudent}");
    }




    public Student SelectedStudent 
    { 
        get => _selectedStudent;
        set
        {
            if (_selectedStudent != value)
            {
                _selectedStudent = value;
                _selectedStudent?.UpdateStudentEvalList();
                OnPropertyChanged(nameof(SelectedStudentEvals));

                //SelectedStudentAverage = _selectedStudent != null && _selectedStudent.SelectedStudentEvals.Any()
                //? $"Average for student {_selectedStudent.DisplayName} is : {_selectedStudent.Average():N2}/20"
                //: "Select a student or add an evaluation";
            }
        }
        //set => _selectedStudent = value; 
    }
    //public Student SelectedStudentEval { get => SelectedStudentEvals; set => selectedStudentEvals = value; }
}