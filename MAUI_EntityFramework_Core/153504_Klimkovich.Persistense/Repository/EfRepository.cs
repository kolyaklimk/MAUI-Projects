using _153504_Klimkovich.Domain.Abstractions;
using _153504_Klimkovich.Domain.Entities;
using _153504_Klimkovich.Persistense.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

//https://metanit.com/sharp/entityframeworkcore/5.6.php

namespace _153504_Klimkovich.Persistense.Repository;

public class EfRepository<T>: IRepository<T> where T : Entity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _entities;
    public EfRepository(AppDbContext context)
    {
        _context = context;
        _entities = _context.Set<T>();
    }

    /// Поиск сущности по Id
    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken =
        default, params Expression<Func<T, object>>[]? includesProperties)
    {
        IQueryable<T>? query = _entities.AsQueryable();

        query = query.Where(e => e.Id == id);

        if(includesProperties.Any())
        {
            foreach(Expression<Func<T, object>>? includeProperty in includesProperties)
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.SingleOrDefaultAsync(cancellationToken);
    }


    /// Получение всего списка сущностей
    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        IQueryable<T>? query = _entities.AsQueryable();
        return await query.ToListAsync(cancellationToken);
    }


    /// Получение отфильтрованного списка
    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includesProperties)
    {
        IQueryable<T>? query = _entities.AsQueryable();
        if(includesProperties.Any())
        {
            foreach(Expression<Func<T, object>>? included in includesProperties)
            {
                query = query.Include(included);
            }
        }
        if(filter != null)
        {
            query = query.Where(filter);
        }
        return await query.ToListAsync();
    }


    /// Добавление новой сущности
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _entities.AddAsync(entity, cancellationToken);
    }


    /// Изменение сущности
    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }


    /// Удаление сущности
    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        return Task.CompletedTask;
    }


    /// Поиск первой сущности, удовлетворяющей условию отбора.
    /// Если сущность не найдена, будет возвращено значение по умолчанию
    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        return await _entities.FirstOrDefaultAsync(filter, cancellationToken);
    }
}
