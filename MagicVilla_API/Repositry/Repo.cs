using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repositry.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MagicVilla_API.Repositry.IRepo;

namespace MagicVilla_API.Repositry
{
    public class Repo<T> : IRepo<T> where T : class
    {



        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;
        public Repo(ApplicationDbContext db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }
        public async Task create(T entity)
        {
            await dbset.AddAsync(entity);
            await Save();
        }

        public async Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbset;
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

        public async Task<List<T>> Getall(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task Remove(T entity)
        {
            dbset.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }



    }
}