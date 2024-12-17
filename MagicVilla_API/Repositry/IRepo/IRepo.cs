using MagicVilla_API.Models;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositry.IRepo
{
    public interface IRepo< T> where T : class
    {
        Task<List<T>> Getall(Expression<Func<T, bool>>? filter = null); //get all
        Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true); //get one of the villas 
        Task create(T entity);
        Task Remove(T entity);


        Task Save();


    }
}
