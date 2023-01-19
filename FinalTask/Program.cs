using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу:");
            string strPath = Console.ReadLine();

            if (!File.Exists(strPath)) 
            {
                Console.WriteLine("Файл не найден!");
                return;
            }

            var arStudents = ReadFile(strPath);
            WriteToDirectory(arStudents);
            Console.WriteLine("База студентов успешно обработана!");
        }

        static public void WriteToDirectory(Student[] arStudents)
        {
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var dir = $"{desktop}\\Students";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            foreach (var student in arStudents)
            {
                var groupFile = $"{dir}\\{student.Group}.txt";
                File.AppendAllText(groupFile, $"{student.Name}, {student.DateOfBirth:dd.MM.yyyy}\r\n");
            }
        }

        static public Student[] ReadFile(string strPath)
        { 
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(strPath, FileMode.Open))
            {
                return (Student[])formatter.Deserialize(stream);
            }
        }
    }

    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
