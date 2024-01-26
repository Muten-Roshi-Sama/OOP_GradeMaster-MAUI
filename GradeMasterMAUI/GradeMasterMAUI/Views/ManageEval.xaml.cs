namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;
using System.ComponentModel;

using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Activity = Models.Activity;

public partial class ManageEval : ContentPage, INotifyPropertyChanged
{
    private List<Activity> _activityList => Activity.GetActivityList(); //CANNOT be static
    private List<Student> _studentList => Student.GetStudentList(); //CANNOT be static
    //public static List<AllEvalList> EvaluationsList => AllEvalList.GetEvaluationsList();
    private List<Eval> _evalList => Eval.GetEvalList(); //CANNOT be static ?? //TODO

    private Student? _selectedStudent;
    private Activity? _selectedActivity;
    //private Eval _selectedEval;




    public ManageEval()
	{
		InitializeComponent();
        Activity.UnpackAll();
        Student.UnpackAll();
        Eval.UnpackAll();
        BindingContext = this;
        DataChangedNotifier.OnDataChanged += UpdateData;
    }
    private void UpdateData()
    {
        Activity.UnpackAll();
        Student.UnpackAll();
        Eval.UnpackAll();
        OnPropertyChanged(nameof(ActivityList));
        OnPropertyChanged(nameof(StudentList));
        OnPropertyChanged(nameof(EvalList));
    }

    private void OnStudentPickerSelectionChanged(object sender, EventArgs eventArgs)
    {
        var studentPicker = sender as Picker;
        if (studentPicker != null && studentPicker.SelectedItem is Student selectedStudent)
        {
            _selectedStudent = selectedStudent; // Assuming selectedProf is a class-level variable
            //Debug.WriteLine($"SelectedProf is {selectedStudent.DisplayName}");
        }
        //Debug.WriteLine($"SelectedProf is {selectedStudent}");
    }
    private void OnActivityPickerSelectionChanged(object sender, EventArgs eventArgs)
    {
        var activityPicker = sender as Picker;
        if (activityPicker != null && activityPicker.SelectedItem is Activity selectedActivity)
        {
            _selectedActivity = selectedActivity; // Assuming selectedProf is a class-level variable
            //Debug.WriteLine($"SelectedProf is {selectedStudent.DisplayName}");
        }
        //Debug.WriteLine($"SelectedProf is {selectedStudent}");
    }
    
    
    private void OnAddEvalClicked(object sender, EventArgs e)
    {
        try
        {
            // Check if _selectedActivity and _selectedStudent are not null
            //if (_selectedActivity == null || _selectedStudent == null)
            //{
            //    throw new NullReferenceException("Activity or Student not selected.");
            //}

            string activityFile = _selectedActivity.GetFileName;
            string studentFile = _selectedStudent.GetFileName;
            string eval = evalEntry.Text;
            int numericalEval;
                        //Debug.WriteLine($"[ManageEval] activityFile is : {activityFile}");
                        //Debug.WriteLine($"[ManageEval] studentFile is : {studentFile}");

            if (eval == "X"||eval=="TB"||eval=="B"||eval=="C"||eval=="N")
            {
                numericalEval = Eval.Note(eval);
            }
            else { numericalEval = Convert.ToInt32(evalEntry.Text);}

            if (numericalEval < 0 || numericalEval > 20)
            {throw new FormatException("Evaluation score must be between 0 and 20.");}


            var newEval = new Eval(eval: numericalEval, studentFile: studentFile, activityFile: activityFile);
            newEval.Pack(); // Save the new student
            Debug.WriteLine("New Eval Added ! [OnAddEvalClicked]");

            // Reset Labels
            errorLabel.IsVisible = false;
            pickerErrorLabel.IsVisible = false;
            evalEntry.Text = string.Empty;

            activityPicker.SelectedItem = null; // Replace 'activityPicker' with your actual picker's name
            studentPicker.SelectedItem = null;
            _selectedActivity = null; // Also reset the backing variable
            _selectedStudent = null;
            studentFile = null;
            activityFile = null;

        }
        catch (FormatException)
        {
            // Handle the case where the input is not a valid integer
            errorLabel.Text = "[Error] Please enter a valid integer for evaluation.";
            errorLabel.IsVisible = true;
            evalEntry.Text = string.Empty;
            
        }
        catch (NullReferenceException)
        {
            pickerErrorLabel.Text = "Please pick a valid Activity and Student.";
            pickerErrorLabel.IsVisible = true;
        }
        catch (Exception ex)
        {
            // Handle other types of exceptions
            errorLabel.Text = $"[Error] Unexpected error: {ex.Message}";
            errorLabel.IsVisible = true;
            evalEntry.Text = string.Empty;
        }




        //Update Data
        Activity.UnpackAll();
        Student.UnpackAll();
        OnPropertyChanged(nameof(ActivityList));
        OnPropertyChanged(nameof(StudentList));
        DataChangedNotifier.NotifyDataChanged();

        //Clear Form
        evalEntry.Text = string.Empty;


    }




    public List<Student> StudentList
    {
        get => _studentList;
    }
    public List<Activity> ActivityList
    {
        get => _activityList;
    }
    public List<Eval> EvalList
    {
        get => _evalList;
    }




}
