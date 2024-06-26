using System.Linq.Expressions;
using ITExperts.DAL.Context;
using ITExperts.DAL.Entities;
using ITExperts.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITExperts.DAL.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
    private ApplicationDbContext _context;
    protected DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
        
    public async Task<T> GetByIdAsync(params object[] id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string[] includeProperties = null,
        int? pageNumber = null, int? pageSize = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            query = query.Skip(pageNumber.Value * pageSize.Value).Take(pageSize.Value);
        }

        return await query.ToListAsync();
    }

    public async Task InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
    
    public async Task<Category> GetByIdWithoutTrackingAsync(int id)
    {
        return await ((_dbSet as DbSet<Category>)!).AsNoTracking().FirstOrDefaultAsync(_ => _.Id == id);
    }
}