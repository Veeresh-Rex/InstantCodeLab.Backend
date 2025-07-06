using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InstantCodeLab.Domain.Entities;
using InstantCodeLab.Domain.Repositories;
using MongoDB.Driver;

namespace InstantCodeLab.Infrastructure.Persistence.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;
    public abstract string CollectionName { get; }

    public IQueryable<T> Query { get; }

    public GenericRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<T>(CollectionName);
        Query = _collection.AsQueryable();
    }

    public async Task<T?> FindFirstOrDefaultAsync(Expression<Func<T, bool>> filter)
    {
        try
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error finding entity: {ex.Message}");
            return null;
        }
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        try
        {
            return await _collection.Find(e => e._id == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting entity by ID: {ex.Message}");
            return null;
        }
    }

    public async Task CreateAsync(T entity) =>
        await _collection.InsertOneAsync(entity);

    public async Task UpdateAsync(string id, T entity) =>
        await _collection.ReplaceOneAsync(e => e._id == id, entity);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(e => e._id == id);

    public async Task DeleteManyAsync(Expression<Func<T, bool>> filter)
    {
        try
        {
            await _collection.DeleteManyAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting entities: {ex.Message}");
        }
    }
}
