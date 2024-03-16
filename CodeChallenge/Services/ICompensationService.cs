using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Compensation Create(string id, int salary, DateTime effectiveDate);
        Compensation GetById(string id);
    }
}
