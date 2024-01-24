namespace GradeMasterMAUI.Views;
using GradeMasterMAUI.Models;
using GradeMasterMAUI.Services;
using System.ComponentModel;
public partial class ManageProfessors : ContentPage, INotifyPropertyChanged
{
    //public ManageProfessors()
    //{

    //}
    public List<Professor> ProfessorList => Professor.GetProfessorList(); //CANNOT be static

    public ManageProfessors()
    {
        InitializeComponent();
        Professor.UnpackAll();
        BindingContext = this;
        DataChangedNotifier.OnDataChanged += UpdateData;
    }

    private void OnAddProfessorClicked(object sender, EventArgs e)
    {
        try
        {
            var firstname = firstnameEntry.Text;
            var lastname = lastnameEntry.Text;
            var salary = Convert.ToInt32(salaryEntry.Text);

            var newProfessor = new Professor(firstname, lastname, salary);
            newProfessor.Pack(); // Save the new student

            //Update Data
            Professor.UnpackAll();
            OnPropertyChanged(nameof(ProfessorList));

            DataChangedNotifier.NotifyDataChanged();

            firstnameEntry.Text = string.Empty;
            lastnameEntry.Text = string.Empty;
            salaryEntry.Text = string.Empty;
        }
        catch (FormatException)
        {
            // Handle the case where the input is not a valid integer
            errorLabel.Text = "[Error] Please enter a valid integer for salary.";
            errorLabel.IsVisible = true;
            salaryEntry.Text = string.Empty;
        }
        catch (Exception ex)
        {
            // Handle other types of exceptions
            errorLabel.Text = $"[Error] Unexpected error: {ex.Message}";
            errorLabel.IsVisible = true;
            salaryEntry.Text = string.Empty;
        }

    }

    private void UpdateData()
    {
        Professor.UnpackAll();
        OnPropertyChanged(nameof(ProfessorList));
    }

}