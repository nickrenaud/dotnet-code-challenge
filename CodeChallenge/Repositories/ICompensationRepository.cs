﻿using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation Add(Employee employee, int salary, DateTime effectiveDate);
        Compensation GetById(string id);
        Task SaveAsync();
    }
}
