using CodeChallenge.DTO;
using System;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public Employee Employee { get; set; }
        public string Salary { get; set; }
        public DateTime EffectiveDate { get; set; }

        public Compensation() { }

        public Compensation(Employee employee, CompensationDto dto)
        {
            Employee = employee;
            Salary = dto.Salary;
            EffectiveDate = dto.EffectiveDate;
        }
    }
}
