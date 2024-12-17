using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repositry.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositry
{
    public class VillaRepo : Repo<Villa>, IVillaRepo
    {
        private readonly ApplicationDbContext _db;
        public VillaRepo(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        
        public async Task<Villa> Update(Villa entity)
        {
          
           
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
          
        }

     
    }
}
