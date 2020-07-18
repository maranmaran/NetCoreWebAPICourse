using Microsoft.EntityFrameworkCore;
using PokemonAPI.DomainLayer;
using PokemonAPI.DomainLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.PersistenceLayer.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly ApplicationDbContext _context;
        private protected readonly DbSet<TEntity> Entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            Entities = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            return await Entities.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await Entities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Guid> Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await Entities.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty guid", nameof(id));

            var entity = await Entities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
