using StudentPickerApp.Models;
using System.Collections.ObjectModel;

namespace StudentPickerApp.Views;

public partial class ClassPage : ContentPage
{
	public ClassPage(string className)
	{
		InitializeComponent();
        var loadedData = HandleData.LoadClassesFromFile(HandleData.dataFilePath);
        var selectedData = loadedData.FirstOrDefault(x => x.Name == className);
        BindingContext = new Models.ClassOfStudents()
        {
            Name = className,
            Students = selectedData.Students ?? new ObservableCollection<Student>() { new Student()},
        };
        ((Models.ClassOfStudents)BindingContext).SortStudents();
        title.Text = className;
    }

    private void DropStudent_clicked(object sender, EventArgs e)
    {
        if (((Button)sender).CommandParameter is string pickedStudent)
        {
            var loadedData = HandleData.LoadClassesFromFile(HandleData.dataFilePath);
            string className = ((Models.ClassOfStudents)BindingContext).Name;
            if (loadedData.FirstOrDefault(x => x.Name == className) != null)
            {
                loadedData.FirstOrDefault(x => x.Name == className).DropStudent(pickedStudent);
            }  
            ((Models.ClassOfStudents)BindingContext).DropStudent(pickedStudent);
            HandleData.SaveClassesToFile(HandleData.dataFilePath, loadedData);
        }
    }
}