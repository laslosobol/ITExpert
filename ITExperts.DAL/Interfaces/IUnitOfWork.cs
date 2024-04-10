using ITExperts.DAL.Entities;

namespace ITExperts.DAL.Interfaces;

public interface IUnitOfWork
{
    IRepository<Film> FilmRepository { get; }
    IRepository<Category> CategoryRepository { get; }
    IRepository<FilmCategory> FilmCategoryRepository { get; }
    
    Task CommitAsync();
    Task RollbackAsync();
}