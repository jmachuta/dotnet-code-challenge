using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.DTO;
using CodeChallenge.Extensions;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository, ICompensationRepository compensationRepository)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        public ReportingStructure GetReportingStructureById(string id)
        {
            var employee = GetById(id);

            if (employee != null)
            {
                int numberOfReports = GetSumOfDirectReports(employee);
                return new ReportingStructure(employee, numberOfReports);
            }

            return null;
        }

        private int GetSumOfDirectReports(Employee employee)
        {
            int reports = 0;

            foreach (var report in employee.DirectReports.AsNullSafe())
            {
                reports++;
                reports += GetSumOfDirectReports(report);
            }

            return reports;
        }

        public Compensation CreateCompensation(Compensation compensation)
        {
            if (compensation != null)
            {
                // Mapping model to a dto that has the employeeId instead of the whole employee object
                // Since employees are persisted elsewhere, we really only need to relate the compensation data with the employeeId
                var compensationDto = new CompensationDto(compensation);
               
                _compensationRepository.Add(compensationDto);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetCompensationById(string id)
        {
            var employee = GetById(id);

            if (employee != null)
            {
                var compensationDto = _compensationRepository.GetById(id);

                return compensationDto != null ? new Compensation(employee, compensationDto) : null;
            }

            return null;
        }
    }
}
