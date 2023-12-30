namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;
using System.ComponentModel;

using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Activity = Models.Activity;

public partial class ManageActivities : ContentPage, INotifyPropertyChanged
{
    public List<Activity> ActivityList => Activity.GetActivityList(); //CANNOT be static
    public List<Professor> ProfessorList => Professor.GetProfessorList();
    public Professor selectedProf;
    public ManageActivities()
	{
		InitializeComponent();
        Activity.UnpackAll();
        Professor.UnpackAll();
        //foreach (var prof in Professor.GetProfessorList())
        //{
        //    professorPicker.Items.Add(prof);
        //}
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
            selectedProf = selectedProfessor; // Assuming selectedProf is a class-level variable
            Debug.WriteLine($"SelectedProf is {selectedProfessor.DisplayName}");
        }
        //selectedProf = professorPicker.SelectedItem;
        Debug.WriteLine($"SelectedProf is {selectedProf}");
    }
    //public Professor SelectedProf
    //{
    //    get => selectedProf;
    //    set
    //    {
    //        if (selectedProf != value)
    //        {
    //            selectedProf = value;
    //        }
    //    }
    //}


    private void OnAddActivityClicked(object sender, EventArgs e)
    {
        string professorFile = selectedProf.FileName;
        Debug.WriteLine($"professorFile is : {professorFile}");
        if (professorFile == null)
        {
            Debug.WriteLine("professorFile is null !");
            return;
        }
        var newActivity = new Activity(activityNameEntry.Text, professorFile, Convert.ToInt32(ectsEntry.Text));
        newActivity.Pack(); // Save the new student
        Debug.WriteLine("New Activity Added !");
        //Update Data
        Activity.UnpackAll();
        OnPropertyChanged(nameof(ActivityList));
        DataChangedNotifier.NotifyDataChanged();

        //Clear Form
        activityNameEntry.Text = string.Empty;
        ectsEntry.Text = string.Empty;


    }

    
    public Professor SelectedProf
    {
        get { return selectedProf; }
    }


    
}