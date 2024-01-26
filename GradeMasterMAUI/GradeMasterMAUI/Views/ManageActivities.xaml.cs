namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;
using System.ComponentModel;

using System.Diagnostics;
using Activity = Models.Activity;

public partial class ManageActivities : ContentPage, INotifyPropertyChanged
{
    // !!!! CANNOT be static
    private List<Activity> _activityList => Activity.GetActivityList(); 
    private List<Professor> _professorList => Professor.GetProfessorList();

    private Professor _selectedProf;


    public ManageActivities()
	{
		InitializeComponent();
        Activity.UnpackAll();
        Professor.UnpackAll();
        BindingContext = this;
        DataChangedNotifier.OnDataChanged += UpdateData;
        
    }
    private void UpdateData()
    {
        Activity.UnpackAll();
        Professor.UnpackAll();
        OnPropertyChanged(nameof(ActivityList));
        OnPropertyChanged(nameof(ProfessorList));
    }
    private void OnPickerSelectionChanged(object sender, EventArgs eventArgs)
    {
        var professorPicker = sender as Picker;
        if (professorPicker != null && professorPicker.SelectedItem is Professor selectedProfessor)
        {
            _selectedProf = selectedProfessor; // Assuming selectedProf is a class-level variable
            Debug.WriteLine($"[ManageActivities] SelectedProf is {selectedProfessor.DisplayName}");
        }
    }


    private void OnAddActivityClicked(object sender, EventArgs e)
    {
        try
        {
            string professorFile = _selectedProf.GetFileName;
            //?? throw new ArgumentNullException("professorFile");
            var activity = activityNameEntry.Text;
                //?? throw new ArgumentNullException("activity");


            var newActivity = new Activity(activity, professorFile, Convert.ToInt32(ectsEntry.Text));
            newActivity.Pack(); // Save the new student
            Debug.WriteLine("[OnAddActivityClicked] New Activity Added !");
            

            //Clear Form
            activityNameEntry.Text = string.Empty;
            ectsEntry.Text = string.Empty;
            errorLabel.IsVisible = false;
            activityErrorLabel.IsVisible = false;

            //professorPicker.SelectedItem = null; 
            //_selectedProf = null;
            //professorFile = null;


        }
        catch (FormatException)
        {
            // Handle the case where the input is not a valid integer
            errorLabel.Text = "[Error] Please enter a valid integer for ECTS.";
            errorLabel.IsVisible = true;
            ectsEntry.Text = string.Empty;
        }
        catch (NullReferenceException)
        {
            activityErrorLabel.Text = "[Error] Please provide an Activity name and pick a professor.";
            activityErrorLabel.IsVisible = true;
        }
        catch (Exception ex)
        {
            errorLabel.Text = $"[Error] Unexpected error: {ex.Message}";
            errorLabel.IsVisible = true;
            ectsEntry.Text = string.Empty;
        }

        //Update Data
        Activity.UnpackAll();
        OnPropertyChanged(nameof(ActivityList));
        DataChangedNotifier.NotifyDataChanged();

    }

    
    public Professor SelectedProf
    {
        get { return _selectedProf; }
    }

    public List<Activity> ActivityList
    {
        get => _activityList;
    }

    public List<Professor> ProfessorList
    {
        get => _professorList;
    }

}