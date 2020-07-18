using AutoMapper;
using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.DTOModels;
using PokemonAPI.PersistenceLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Implementations
{
    internal class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _repository;
        private readonly IMapper _mapper;

        public PokemonService(IPokemonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PokemonDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            var pokemon = await _repository.GetAll(cancellationToken);

            return _mapper.Map<IEnumerable<PokemonDTO>>(pokemon);
        }

        public async Task<PokemonDTO> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var pokemon = await _repository.GetById(id, cancellationToken);
            if (pokemon == null)
                throw new NotFoundException(id);

            return _mapper.Map<PokemonDTO>(pokemon);
        }

        public async Task<Guid> Create(PokemonDTO pokemon, CancellationToken cancellationToken = default)
        {
            try
            {
                var pokemonEntity = _mapper.Map<Pokemon>(pokemon);
                return await _repository.Insert(pokemonEntity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
        }

        public async Task Update(PokemonDTO pokemon, CancellationToken cancellationToken = default)
        {
            try
            {
                var pokemonEntity = _mapper.Map<Pokemon>(pokemon);

                await _repository.Update(pokemonEntity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new UpdateException(pokemon.Id, e);
            }
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _repository.Delete(id, cancellationToken);
            }
            catch (Exception e)
            {
                throw new DeleteException(id, e);
            }
        }
    }
}
