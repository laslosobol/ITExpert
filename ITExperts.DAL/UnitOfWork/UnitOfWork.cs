using ITExperts.DAL.Context;
using ITExperts.DAL.Entities;
using ITExperts.DAL.Interfaces;
using ITExperts.DAL.Repositories;

namespace ITExperts.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;

    private IRepository<Film> _filmRepository;
    public IRepository<Film> FilmRepository => _filmRepository ??= new GenericRepository<Film>(_context);
    
    private IRepository<Category> _categoryRepository;
    public IRepository<Category> CategoryRepository => _categoryRepository ??= new GenericRepository<Category>(_context);
    
    private IRepository<FilmCategory> _filmCategoryRepository;
    public IRepository<FilmCategory> FilmCategoryRepository => _filmCategoryRepository ??= new GenericRepository<FilmCategory>(_context);
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        await _context.DisposeAsync();
    }

    private bool disposed = false;
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);

    }

    public void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }
}