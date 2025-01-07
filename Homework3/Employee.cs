using System;

namespace BusinessApp
{
    public class Employee : Entity
    {
        private string department;
        private double salary;

        public string Department { get => department; set => department = value; }
        public double Salary { get => salary; set => salary = value; }

        public Employee(int id, string name, string department, double salary)
            : base(id, name)
        {
            this.department = department;
            this.salary = salary;
        }

        public override string ToString()
        {
            return base.ToString() + $", Department: {department}, Salary: {salary:C}";
        }
    }
}
