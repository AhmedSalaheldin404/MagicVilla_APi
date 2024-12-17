using MagicVilla_API.Models;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositry.IRepo
{
    public interface IVillaRepo:IRepo<Villa>
    {

     
        Task<Villa> Update(Villa entity);

        



    }
}
