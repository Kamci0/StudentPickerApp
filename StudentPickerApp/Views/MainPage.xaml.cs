using StudentPickerApp.Models;
using System.Collections.ObjectModel;

namespace StudentPickerApp.Views;

public partial class MainPage : ContentPage
{
    string temp_name, temp_surname, temp_className, temp_pickedClass, temp_selectedClass;
	public MainPage()
	{
		InitializeComponent();
        var loadedData = HandleData.LoadClassesFromFile(HandleData.dataFilePath);
        BindingContext = new Models.ListOfCLasses()
        { 
            AllClasses = loadedData ?? new ObservableCollection<ClassOfStudents> { new ClassOfStudents() },
        };

        List<string> classesNameList = new();
        foreach (ClassOfStudents classData in ((Models.ListOfCLasses)BindingContext).AllClasses)
        {  
            classesNameList.Add(classData.Name);
        }

        PickAClass.ItemsSource = classesNameList;
        ClassesList.ItemsSource = classesNameList;
	}

    //add student
    private void NameEntry_textchanged(object sender, TextChangedEventArgs e)
    {
        temp_name = ((Entry)sender).Text;
    }
    private void SurnameEntry_textchanged(object sender, TextChangedEventArgs e)
    {
        temp_surname = ((Entry)sender).Text;
    }
    private void ClassPicker_selectedIndexChanged(object sender, EventArgs e)
    {
        temp_pickedClass = ((Picker)sender).SelectedItem as string;
    }
    private void AddButton_clicked(object sender, EventArgs e)
    {
        ((ListOfCLasses)BindingContext).AllClasses.FirstOrDefault(c => c.Name == temp_pickedClass).Students.Add(new Student() { Name = temp_name, Surname = temp_surname });
        HandleData.SaveClassesToFile(HandleData.dataFilePath, ((ListOfCLasses)BindingContext).AllClasses);
        name_entry.Text = string.Empty;
        surname_entry.Text = string.Empty;
    }

    //create class
    private void ClassNameEntry_textchanged(object sender, TextChangedEventArgs e)
    {
        temp_className = ((Entry)sender).Text;
    }
    private void AddClassButton_clicked(object sender, EventArgs e)
    {
        var listOfClasses = (ListOfCLasses)BindingContext;
        listOfClasses.AddClass(temp_className, new ObservableCollection<Student>());
        HandleData.SaveClassesToFile(HandleData.dataFilePath, ((ListOfCLasses)BindingContext).AllClasses);
    }


    //class details
    private void ClassesCollectronView_selectronChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Any())
        {
            var selectedClass = e.CurrentSelection.FirstOrDefault();
            temp_selectedClass = selectedClass.ToString();
            classDetails.IsVisible = true;
            randomStudent.IsVisible = true;
        }
    }
    private void ShowClassDetails_clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ClassPage(temp_selectedClass));
    }

    private void RandomStudent_clicked(object sender, EventArgs e)
    {
        var selectedClass = ((ListOfCLasses)BindingContext).AllClasses.FirstOrDefault(x => x.Name == temp_selectedClass);
        selectedClass.SortStudents();
        Student s = selectedClass.StudentLottery();
        
        if (s != null)
        {
            pickedStudent.Text = string.Format("Imie: {0}, Nazwisko {1}, Numer w dzienniku, {2}", s.Name, s.Surname, selectedClass.Students.IndexOf(s)+1);
        }
    }
}