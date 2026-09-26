using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalepYonetimi.Domain.Interfaces;
using TalepYonetimi.Infrastructure.Data;

namespace TalepYonetimi.Infrastructure.Repositories
{
    public class Repository<T>(ApplicationDbContext dbContext) : IRepository<T> where T : class
    {
        protected ApplicationDbContext DbContext { get; } = dbContext;
        protected DbSet<T> DbSet { get; } = dbContext.Set<T>();
        
        public async Task EkleAsync(T entity, CancellationToken cancellationToken = default)
        
           => await DbSet.AddAsync(entity, cancellationToken);

        public Task<T?> IdYeGoreGetirAsync(int id, CancellationToken cancellationToken = default)
        => DbSet.FindAsync([id], cancellationToken).AsTask();
       

        public async Task<IReadOnlyList<T>> ListeleAsync(CancellationToken cancellationToken = default)
        
           =>  await DbSet.AsNoTracking().ToListAsync(cancellationToken);
        
        public void Güncelle(T entity) => DbSet.Update(entity);

        public void Sil(T entity) => DbSet.Remove(entity);
       
    }
}
