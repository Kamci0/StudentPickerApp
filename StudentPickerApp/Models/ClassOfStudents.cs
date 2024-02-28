using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPickerApp.Models
{
    internal class ClassOfStudents : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();

        public void AddStudent(string name, string surname)
        {
            Students.Add(new Student() { Name = name, Surname = surname});
        }

        public void DropStudent(string surname) { 
            var studentToRemove = Students.First(Student => Student.Surname == surname);

            if (studentToRemove != null)
            {
                Students.Remove(studentToRemove);
            }
        }

        public Student StudentLottery()
        {
            var sortedStudents = Students.OrderBy(student => student.Surname).ToList();
            Students.Clear();
            foreach (var student in sortedStudents)
            {
                Students.Add(student);
            }

            Random rand = new();

            int selectedIndex = rand.Next(0, Students.Count);

            Student selectedStudent = Students[selectedIndex];

            return selectedStudent;
        }

        public void SortStudents()
        {
            var sortedStudents = Students.OrderBy(student => student.Surname).ToList();
            Students.Clear();
            foreach (var student in sortedStudents)
            {
                Students.Add(student);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
