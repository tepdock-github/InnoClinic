﻿using Authorization.Domain;
using Authorization.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public void Create(T entity) => RepositoryContext.Set<T>()
            .Add(entity);

        public void Delete(T entity) => RepositoryContext.Set<T>()
            .Remove(entity);

        public IQueryable<T> FindAll(bool TrackChanges) =>
            !TrackChanges ?
                RepositoryContext.Set<T>()
                    .AsNoTracking() :
                RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression,
            bool trackChanges) =>
            !trackChanges ?
                RepositoryContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking() :
                RepositoryContext.Set<T>()
                    .Where(expression);

        public void Update(T entity) => RepositoryContext.Set<T>()
            .Update(entity);
    }
}
