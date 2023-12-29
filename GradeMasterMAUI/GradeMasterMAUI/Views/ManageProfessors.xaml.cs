namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;
public partial class ManageProfessors : ContentPage
{
    public List<Professor> ProfessorList =>Professor.GetProfessorList(); //CANNOT be static

    public ManageProfessors()
    {
        InitializeComponent();
        Professor.UnpackAll();
        BindingContext = this;
        DataChangedNotifier.OnDataChanged += UpdateData;
    }

    private void OnAddProfessorClicked(object sender, EventArgs e)
    {
        var newProfessor = new Professor(firstnameEntry.Text, lastnameEntry.Text, Convert.ToInt32(salaryEntry.Text));
        newProfessor.Pack(); // Save the new student

        //Update Data
        Professor.UnpackAll();
        OnPropertyChanged(nameof(ProfessorList));

        DataChangedNotifier.NotifyDataChanged();

        firstnameEntry.Text = string.Empty;
        lastnameEntry.Text = string.Empty;
        salaryEntry.Text = string.Empty;

    }

    private void UpdateData()
    {
        Professor.UnpackAll();
        OnPropertyChanged(nameof(ProfessorList));
    }

}