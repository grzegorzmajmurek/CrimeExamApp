using CrimeService.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrimeService.Repositories
{
    public class CrimesRepository : ICrimesRepository
    {
        private const string collectionName = "crimes";
        private readonly IMongoCollection<Crime> dbCollection;
        private readonly FilterDefinitionBuilder<Crime> filterBuilder = Builders<Crime>.Filter;

        public CrimesRepository(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Crime>(collectionName);
        }

        public async Task<IReadOnlyCollection<Crime>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Crime> GetAsync(Guid id)
        {
            FilterDefinition<Crime> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Crime entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Crime entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<Crime> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }
    }
}
