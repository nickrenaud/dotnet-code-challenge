using System;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Data;
using CodeChallenge.Models;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;

        public CompensationRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext; 
        }

        public Compensation Add(Employee employee, int salary, DateTime effectiveDate)
        {
            var compensation = new Compensation()
            {
                CompensationId = Guid.NewGuid().ToString(),
                Employee = employee,
                Salary = salary,
                EffectiveDate = effectiveDate
            };
            _employeeContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetById(string id)
        {
            return _employeeContext.Compensations.SingleOrDefault(e => e.Employee.EmployeeId == id);
        }
        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

    }
}
