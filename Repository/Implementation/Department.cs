﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Xml.Serialization;
using Homework_11.Entity;
using Newtonsoft.Json;

namespace Homework_11.Repository.Implementation
{
    /// <summary>
    /// Департамент
    /// </summary>
    public class Department : IDepartment
    {
        /// <summary>
        /// Родительский департамент
        /// </summary>
        public Department ParentDepartment { get; set; }
        public string Name { get; set; } = "UNDEFINED";
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        
        public ObservableCollection<Worker> Workers { get; set; } = new ObservableCollection<Worker>();
        
        public ObservableCollection<Department> Departments { get; set; } = new ObservableCollection<Department>();

        [XmlIgnore]
        [JsonIgnore]
        public IList Entities =>
            new CompositeCollection()
            {
                new CollectionContainer() { Collection = Departments },
                new CollectionContainer() { Collection = Workers }
            };

        public Department()
        {
            Name = "HEAD DEPARTMENT";
        }

        public Department(Department parentDepartment)
        {
            ParentDepartment = parentDepartment;
        }

        public Department(Department parentDepartment, string name, ObservableCollection<Worker> workers, ObservableCollection<Department> departments)
        {
            ParentDepartment = parentDepartment;
            Name = name;
            Workers = workers;
            Departments = departments;
        }

        /*
         * Добавление сотрудников/департаментов
         */

        public void add(Worker worker)
        {
            if (Workers.Count >= 1_000_000)
            {
                return;
            }
            Workers.Add(worker);
            worker.Department = this;
        }

        public void add(Department department)
        {
            Departments.Add(department);
            department.ParentDepartment = this;
        }

        public void add(ObservableCollection<Worker> workers)
        {
            foreach (Worker worker in workers)
            {
                Workers.Add(worker);
            }
        }

        public void add(ObservableCollection<Department> departments)
        {
            foreach (Department department in departments)
            {
                Departments.Add(department);
            }
        }

        /*
         * Сортировка сотрудников
         */

        public void sortWorkersByLastName()
        {
            Workers = new ObservableCollection<Worker>(Workers.OrderBy(o => o.LastName));
        }

        public void sortWorkersByFirstName()
        {
            Workers = new ObservableCollection<Worker>(Workers.OrderBy(o => o.FirstName).ToList());
        }

        public void sortWorkersByAge()
        {
            Workers = new ObservableCollection<Worker>(Workers.OrderBy(o => o.Age).ToList());
        }

        public void sortWorkersBySalary()
        {
            Workers = new ObservableCollection<Worker>(Workers.OrderBy(o => o.Salary).ToList());
        }

        public void sortWorkersByProjectsCount()
        {
            Workers = new ObservableCollection<Worker>(Workers.OrderBy(o => o.ProjectsCount).ToList());
        }

        /*
         * Сортировка вложенных департаментов
         */

        public void sortDepartmentsByDateOfCreation()
        {
            Departments = new ObservableCollection<Department>(Departments.OrderBy(o => o.DateOfCreation).ToList());
        }

        public void sortDepartmentsByName()
        {
            Departments = new ObservableCollection<Department>(Departments.OrderBy(o => o.Name).ToList());
        }

        public void sortDepartmentsByWorkersCount()
        {
            Departments = new ObservableCollection<Department>(Departments.OrderBy(o => o.Workers.Count).ToList());
        }

        /*
         * Нахождение сотрудников
         */

        public ObservableCollection<Worker> findWorkersByLastName(string lastName)
        {
            // Массив подошедших под условие сотрудников
            ObservableCollection<Worker> matchWorkers = new ObservableCollection<Worker>();

            // Если фамилия сотрудника совпадает с указанной, добавляем этого сотрудника в массив
            foreach (Worker worker in Workers)
            {
                if (worker.LastName == lastName)
                {
                    matchWorkers.Add(worker);
                }
            }

            return matchWorkers;
        }

        public ObservableCollection<Worker> findWorkersByFirstName(string firstName)
        {
            // Массив подошедших под условие сотрудников
            ObservableCollection<Worker> matchWorkers = new ObservableCollection<Worker>();

            // Если имя сотрудника совпадает с указанным, добавляем этого сотрудника в массив
            foreach (Worker worker in Workers)
            {
                if (worker.FirstName == firstName)
                {
                    matchWorkers.Add(worker);
                }
            }

            return matchWorkers;
        }

        public ObservableCollection<Worker> findWorkersByAge(int age)
        {
            // Массив подошедших под условие сотрудников
            ObservableCollection<Worker> matchWorkers = new ObservableCollection<Worker>();

            // Если возраст сотрудника совпадает с указанным, добавляем этого сотрудника в массив
            foreach (Worker worker in Workers)
            {
                if (worker.Age == age)
                {
                    matchWorkers.Add(worker);
                }
            }

            return matchWorkers;
        }

        public ObservableCollection<Worker> findWorkersBySalary(int salary)
        {
            // Массив подошедших под условие сотрудников
            ObservableCollection<Worker> matchWorkers = new ObservableCollection<Worker>();

            // Если зарплата сотрудника совпадает с указанной, добавляем этого сотрудника в массив
            foreach (Worker worker in Workers)
            {
                if (worker.Salary == salary)
                {
                    matchWorkers.Add(worker);
                }
            }

            return matchWorkers;
        }

        public ObservableCollection<Worker> findWorkersByProjectsCount(int projectsCount)
        {
            // Массив подошедших под условие сотрудников
            ObservableCollection<Worker> matchWorkers = new ObservableCollection<Worker>();

            // Если количество проектов сотрудника совпадает с указанным, добавляем этого сотрудника в массив
            foreach (Worker worker in Workers)
            {
                if (worker.ProjectsCount == projectsCount)
                {
                    matchWorkers.Add(worker);
                }
            }

            return matchWorkers;
        }

        /*
         * Нахождение вложенных департаментов
         */

        public ObservableCollection<Department> findDepartmentsByDateOfCreation(DateTime dateOfCreation)
        {
            // Массив подошедших под условие департаментов
            ObservableCollection<Department> matchDepartments = new ObservableCollection<Department>();

            // Если дата создания департамента совпадает с указанной, добавляем этот департамент в массив
            foreach (Department department in Departments)
            {
                if (department.DateOfCreation == dateOfCreation)
                {
                    matchDepartments.Add(department);
                }
            }

            return matchDepartments;
        }

        public ObservableCollection<Department> findDepartmentsByName(string name)
        {
            // Массив подошедших под условие департаментов
            ObservableCollection<Department> matchDepartments = new ObservableCollection<Department>();

            // Если название департамента совпадает с указанным, добавляем этот департамент в массив
            foreach (Department department in Departments)
            {
                if (department.Name == name)
                {
                    matchDepartments.Add(department);
                }
            }

            return matchDepartments;
        }

        public ObservableCollection<Department> findDepartmentsByWorkersCount(int workersCount)
        {
            // Массив подошедших под условие департаментов
            ObservableCollection<Department> matchDepartments = new ObservableCollection<Department>();

            // Если количество сотрудников департамента совпадает с указанным, добавляем этот департамент в массив
            foreach (Department department in Departments)
            {
                if (department.Workers.Count == workersCount)
                {
                    matchDepartments.Add(department);
                }
            }

            return matchDepartments;
        }

        /*
         * Удаление сотрудников/департаментов
         */

        public void remove(Worker worker)
        {
            Workers.Remove(worker);
        }

        public void remove(Department department)
        {
            Departments.Remove(department);
        }

        public void remove(ObservableCollection<Worker> workers)
        {
            foreach (Worker worker in workers)
            {
                Workers.Remove(worker);
            }
        }

        public void remove(ObservableCollection<Department> departments)
        {
            foreach (Department department in departments)
            {
                Departments.Remove(department);
            }
        }

        public override string ToString()
        {
            string result = $"Name: {Name.ToUpper()}\n";
            result += $"Date of creation: {DateOfCreation}\n";
            result += $"Number of workers: {Workers.Count}\n";
            result += $"Number of nested departments: {Departments.Count}";

            return result;
        }
    }
}