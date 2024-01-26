namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;
using System.Diagnostics;
using Activity = Models.Activity;

public partial class MainPage : ContentPage
{
    private List<Activity> _activityList => Activity.GetActivityList(); //CANNOT be static
    private List<Professor> _professorList => Professor.GetProfessorList();

    public MainPage()
    {
        InitializeComponent();
        Activity.UnpackAll();
        Debug.WriteLine($"ActivityList Count : {_activityList.Count}");
        BindingContext = this;
        UpdateData();
        DataChangedNotifier.OnDataChanged += UpdateData;

    }
    private void UpdateData()
    {
        Activity.UnpackAll();
        Professor.UnpackAll();
        OnPropertyChanged(nameof(_activityList));
        OnPropertyChanged(nameof(_professorList));
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