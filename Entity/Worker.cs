﻿using System;
using System.Xml.Serialization;
using Homework_11.Converter;
using Homework_11.Repository;
using Homework_11.Repository.Implementation;
using Newtonsoft.Json;

namespace Homework_11.Entity
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Worker
    {
        //[JsonConverter(typeof(DepartmentConverterJson<Department>))]
        public Department Department { get; set; }
        
        public string LastName { get; set; } = "Undefined";

        public string FirstName { get; set; } = "Undefined";

        public int Age { get; set; } = 0;

        public int Salary { get; set; } = 0;

        public int ProjectsCount { get; set; } = 0;

        public Worker()
        {

        }

        public Worker(Department department)
        {
            Department = department;
        }

        public Worker(Department department, string lastName, string firstName, int age, int salary, int projectsCount)
        {
            Department = department;
            LastName = lastName;
            FirstName = firstName;
            Age = age;
            Salary = salary;
            ProjectsCount = projectsCount;
        }

        public override string ToString()
        {
            string result = $"Last name: {LastName}\n";
            result += $"First name: {FirstName}\n";
            result += $"Age: {Age}\n";
            result += $"Salary: {Salary}\n";
            result += $"ProjectsCount: {ProjectsCount}";

            return result;
        }
    }
}