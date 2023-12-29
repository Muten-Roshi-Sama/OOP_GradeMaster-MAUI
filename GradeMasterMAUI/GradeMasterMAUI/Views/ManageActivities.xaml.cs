namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;
using System.ComponentModel;

using System.Diagnostics;
using Activity = Models.Activity;

public partial class ManageActivities : ContentPage, INotifyPropertyChanged
{
    public List<Activity> ActivityList => Activity.GetActivityList(); //CANNOT be static
    public List<Professor> ProfessorList => Professor.GetProfessorList();
    private Professor selectedProf;
    public ManageActivities()
	{
		InitializeComponent();
        Activity.UnpackAll();
        //foreach (var prof in Professor.GetProfessorList())
        //{
        //    professorPicker.Items.Add(prof.Lastname);
        //}
        BindingContext = this;
        DataChangedNotifier.OnDataChanged += UpdateData;
        
    }

    //private void OnProfessorSelectedIndexChanged(object sender, EventArgs eventArgs)
    //{
    //    var selectedProf = professorPicker.SelectedItem?.ToString();
    //}
    public Professor SelectedProf
    {
        get => selectedProf;
        set
        {
            if (selectedProf != value)
            {
                selectedProf = value;
            }
        }
    }


    private void OnAddActivityClicked(object sender, EventArgs e)
    {
        string professorFile = SelectedProf.FileName;


        //Update Data
        Activity.UnpackAll();
        OnPropertyChanged(nameof(ActivityList));
        DataChangedNotifier.NotifyDataChanged();
        if (professorFile == null)
        {
            return;
        }
        var newActivity = new Activity(activityNameEntry.Text, professorFile, Convert.ToInt32(ectsEntry.Text));
        newActivity.Pack(); // Save the new student
        Debug.WriteLine("New Activity Added !");

        

    }

    private void UpdateData()
    {
        Activity.UnpackAll();
        Professor.UnpackAll();
        OnPropertyChanged(nameof(ActivityList));
        OnPropertyChanged(nameof(ProfessorList));
    }



    
}