using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using System.Runtime.InteropServices;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly ILogger _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public ReportingStructure GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return Create(id);
            }

            return null;
        }

        public ReportingStructure Create(string id)
        {
            var employee = _employeeRepository.GetById(id);

            return new ReportingStructure()
            {
                Employee = employee,
                NumberOfReports = GetNumberOfReports(employee),
            };
        }

        private int GetNumberOfReports(Employee employee)
        {
            var reports = employee.DirectReports?.Count ?? 0;

            if (reports > 0)
            {
                foreach (Employee e in employee.DirectReports)
                {
                    reports += GetNumberOfReports(e);
                }
            }

            return reports;
        }
    }
}
