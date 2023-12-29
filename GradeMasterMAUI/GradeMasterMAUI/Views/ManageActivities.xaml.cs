namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;


public partial class ManageActivities : ContentPage
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
        
    }


    private void OnAddActivityClicked(object sender, EventArgs e)
    {
        var newActivity = new Activity(activityNameEntry.Text, SelectedProf.FileName, Convert.ToInt32(ectsEntry));
        newActivity.Pack(); // Save the new student

        //Update Data
        Activity.UnpackAll();
        OnPropertyChanged(nameof(ActivityList));
        DataChangedNotifier.NotifyDataChanged();

    }

    private void UpdateData()
    {
        Activity.UnpackAll();
        Professor.UnpackAll();
        OnPropertyChanged(nameof(ActivityList));
        OnPropertyChanged(nameof(ProfessorList));
    }



    
}