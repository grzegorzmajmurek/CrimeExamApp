using CrimeService.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrimeService.Repositories
{
    public interface ICrimesRepository
    {
        Task CreateAsync(Crime entity);
        Task<IReadOnlyCollection<Crime>> GetAllAsync();
        Task<Crime> GetAsync(Guid id);
        Task UpdateAsync(Crime entity);
    }
}
