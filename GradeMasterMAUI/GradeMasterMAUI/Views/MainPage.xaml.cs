namespace GradeMasterMAUI.Views;

using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;
using System.ComponentModel;

using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Activity = Models.Activity;

public partial class MainPage : ContentPage
{
    public List<Activity> ActivityList => Activity.GetActivityList(); //CANNOT be static
    public List<Professor> ProfessorList => Professor.GetProfessorList();
    public MainPage()
	{
		InitializeComponent();
        Activity.UnpackAll();
        Debug.WriteLine($"ActivityList Count : {ActivityList.Count}");
        //foreach (var prof in Professor.GetProfessorList())
        //{
        //    professorPicker.Items.Add(prof);
        //}
        BindingContext = this;
        UpdateData();
        DataChangedNotifier.OnDataChanged += UpdateData;

    }
    private void UpdateData()
    {
        Activity.UnpackAll();
        Professor.UnpackAll();
        OnPropertyChanged(nameof(ActivityList));
        OnPropertyChanged(nameof(ProfessorList));
    }
}