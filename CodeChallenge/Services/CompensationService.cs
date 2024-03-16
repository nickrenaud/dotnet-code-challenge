using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        
        public CompensationService(ICompensationRepository compensationRepository, IEmployeeRepository employeeRepository)
        {
            _compensationRepository = compensationRepository;   
            _employeeRepository = employeeRepository;
        }

        public Compensation Create(string id, int salary, DateTime effectiveDate)
        {
            var compensation = new Compensation();
            var employee = _employeeRepository.GetById(id);

            if(employee != null)
            {
                compensation = _compensationRepository.Add(employee, salary, effectiveDate);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }

            return null;
        }
    }
}
