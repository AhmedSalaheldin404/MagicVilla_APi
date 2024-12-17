using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repositry.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositry
{
    public class VillaRepo : IVillaRepo
    {
        private readonly ApplicationDbContext _db;
        public VillaRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task create(Villa entity)
        {
          await _db.Villas.AddAsync(entity);
            await Save();
        }

        public  async Task<Villa> Get(Expression<Func<Villa,bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> query = _db.Villas;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Villa>> Getall(Expression<Func<Villa,bool>> filter = null)
        {
            IQueryable<Villa> query = _db.Villas;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task Remove(Villa entity)
        {
            _db.Villas.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();        }
    }
}
