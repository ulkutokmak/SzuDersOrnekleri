using CommonObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Creater
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            Random rnd = new Random();

            for (int i = 1; i <= 100; i++)
            {
                employees.Add(new Employee
                {
                    Id = 1,
                    Name = $"Name {i}",
                    Age = rnd.Next(18, 65)
                });
            }

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "employees.dat");

            using (var stream = File.OpenWrite(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, employees);
            }
        }
    }
}
