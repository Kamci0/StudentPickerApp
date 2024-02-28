using StudentPickerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StudentPickerApp
{
    internal class HandleData
    {

            public static string dataFilePath = Path.Combine(FileSystem.AppDataDirectory, "Data.txt");
            public static void SaveClassesToFile(string filePath, ObservableCollection<ClassOfStudents> allClasses)
            {
                List<ClassData> classDataList = new List<ClassData>();

                foreach (var classObj in allClasses)
                {
                    ClassData classData = new ClassData
                    {
                        ClassName = classObj.Name,
                        Students = classObj.Students.ToList()
                    };
                    classDataList.Add(classData);
                }

                string json = JsonConvert.SerializeObject(classDataList, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }

            public static ObservableCollection<ClassOfStudents> LoadClassesFromFile(string filePath)
            {
                ObservableCollection<ClassOfStudents> allClasses = new ObservableCollection<ClassOfStudents>();

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    List<ClassData> classDataList = JsonConvert.DeserializeObject<List<ClassData>>(json);

                    foreach (var classData in classDataList)
                    {
                        ClassOfStudents classObj = new ClassOfStudents
                        {
                            Name = classData.ClassName,
                            Students = new ObservableCollection<Student>(classData.Students)
                        };
                        allClasses.Add(classObj);
                    }
                }

                return allClasses;
            }

    }
}
