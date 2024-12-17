using MagicVilla_API.Models;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositry.IRepo
{
    public interface IVillaRepo
    {

        Task<List<Villa>> Getall(Expression<Func<Villa,bool>> filter=null); //get all
        Task<Villa> Get(Expression<Func<Villa,bool>> filter=null,bool tracked=true); //get one of the villas 
        Task create(Villa entity);
        Task Remove(Villa entity);
        Task Update(Villa entity);

        Task Save();



    }
}
