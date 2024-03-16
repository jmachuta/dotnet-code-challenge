using CodeChallenge.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.DTO
{
    public class CompensationDto
    {
        [Key]
        public string EmployeeId { get; set; }
        public string Salary { get; set; }
        public DateTime EffectiveDate { get; set; }

        public CompensationDto() { }

        public CompensationDto(Compensation compensation)
        {
            EmployeeId = compensation.Employee.EmployeeId;
            Salary = compensation.Salary;
            EffectiveDate = compensation.EffectiveDate;
        }
    }
}
