using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PokemonAPI.DomainLayer;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.DomainLayer.Interfaces;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.PersistenceLayer.Repositories
{
    /// <summary>
    /// Overload with TProjection optional
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProjection"></typeparam>
    internal class Repository<TEntity, TProjection> : IRepository<TEntity, TProjection>
    where TEntity : EntityBase, IEntity
    where TProjection : class
    {
        private readonly ApplicationDbContext _context;
        private protected readonly DbSet<TEntity> Entities;
        private readonly IMapper _mapper;

        public Repository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            Entities = _context.Set<TEntity>();
        }
        public async Task<IEnumerable<TProjection>> GetAll(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
        {
            var entities = Entities.AsQueryable();

            if (disableTracking)
            {
                entities = entities.AsNoTracking();
            }

            if (include != null)
            {
                entities = include(entities);
            }

            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            if (orderBy != null)
            {
                entities = orderBy(entities);
            }

            return await entities.ProjectTo<TProjection>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }

        public async Task<TProjection> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
        {
            var entities = Entities.AsQueryable();

            if (disableTracking)
            {
                entities = entities.AsNoTracking();
            }

            if (include != null)
            {
                entities = include(entities);
            }

            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            if (orderBy != null)
            {
                entities = orderBy(entities);
            }

            return await entities.ProjectTo<TProjection>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
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
