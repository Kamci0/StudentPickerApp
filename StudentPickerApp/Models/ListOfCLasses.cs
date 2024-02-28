using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentPickerApp.Models
{
    internal class ListOfCLasses : INotifyPropertyChanged
    {
        private ObservableCollection<ClassOfStudents> allClasses;
        public ObservableCollection<ClassOfStudents> AllClasses
        {
            get { return allClasses; }
            set
            {
                if (value != allClasses)
                {
                    allClasses = value;
                    OnPropertyChanged();
                }
            }
        }

        public ListOfCLasses()
        {
            AllClasses = new ObservableCollection<ClassOfStudents>();
        }

        public void AddClass(string name, ObservableCollection<Student> list)
        {
            AllClasses.Add(new ClassOfStudents() { Name = name, Students = list });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
