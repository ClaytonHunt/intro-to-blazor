using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QuestList.Shared.Models;

namespace QuestList.Shared.Interfaces
{
    public interface IRepository<T>
    {
        Task<int> Create(T item);
        Task<T> ReadById(int id);
        Task<IList<T>> ReadAll(Func<T, bool> predicate);
        Task<int> Update(T item);
        Task Delete(T item);
        IRepository<T> Include(Expression<Func<QuestLine, object>> path);
    }
}