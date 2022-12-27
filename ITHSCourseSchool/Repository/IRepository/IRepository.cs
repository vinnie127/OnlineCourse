﻿using System.Linq.Expressions;

namespace ITHSCourseSchool.Repository.IRepository
{
    public interface IRepository<T> where T : class 
    {

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
        Task SaveAsync();





    }
}
