﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InstantCodeLab.Domain.Entities;
using MongoDB.Driver;

namespace InstantCodeLab.Domain.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    IQueryable<T> Query { get; }
    Task<T?> FindFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
    Task<T?> GetByIdAsync(string id);
    Task CreateAsync(T entity);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
    Task DeleteManyAsync(Expression<Func<T, bool>> filter);
    Task BulkWriteAsync(IEnumerable<WriteModel<T>> operations);
}
