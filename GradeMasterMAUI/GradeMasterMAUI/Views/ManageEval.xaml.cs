namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;
using System.ComponentModel;

using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Activity = Models.Activity;

public partial class ManageEval : ContentPage, INotifyPropertyChanged
{
    public List<Activity> ActivityList => Activity.GetActivityList(); //CANNOT be static
    public List<Student> StudentList => Student.GetStudentList(); //CANNOT be static
    //public static List<AllEvalList> EvaluationsList => AllEvalList.GetEvaluationsList();
    public List<Eval> EvalList => Eval.GetEvalList(); //CANNOT be static ?? //TODO

    private Student _selectedStudent;
    private Activity _selectedActivity;
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
        string activityFile = _selectedActivity.FileName;
        string studentFile = _selectedStudent.FileName;
        Debug.WriteLine($"[ManageEval] activityFile is : {activityFile}");
        Debug.WriteLine($"[ManageEval] studentFile is : {studentFile}");

        var eval = Convert.ToInt32(evalEntry.Text);

        if (eval >= 0 && eval <= 20)
        {
            var newEval = new Eval(eval: eval, studentFile: studentFile, activityFile: activityFile);
            //new AllEvalList(_selectedStudent, eval);
            //_selectedEval = newEval;
            //Eval(int eval, string studentFile, string activityFile)
            newEval.Pack(); // Save the new student
            Debug.WriteLine("New Eval Added ! [OnAddEvalClicked]");
            errorLabel.IsVisible = false;
        }
        else
        {
            errorLabel.Text = "[Error] Evaluation should be between 0 and 20.";
            errorLabel.IsVisible = true;
            //Clear Form
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

    //public Student SelectedStudent
    //{
    //    get { return _selectedStudent; }
    //}
    //public Activity SelectedActivity
    //{
    //    get { return _selectedActivity; }
    //}

    //public Eval SelectedEval
    //{
    //    get { return _selectedEval; }
    //}

}
