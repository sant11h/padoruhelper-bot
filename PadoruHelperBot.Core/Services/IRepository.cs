using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PadoruHelperBot.Core.Services
{
    public interface IRepository<T> where T: class
    {
        Task Add(T entity);
        Task Remove(T entity);
        Task<List<T>> GetAll();
    }
}
