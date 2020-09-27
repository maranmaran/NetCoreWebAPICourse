using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.DTOModels;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Implementations.DomainServices
{
    internal class AbillityService : IAbillityService
    {
        private readonly IRepository<PokemonAbility> _repository;
        private readonly IMapper _mapper;

        public AbillityService(IRepository<PokemonAbility> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AbilityDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            var abilities = await _repository.GetAll(
                include: source => source
                    .Include(x => x.Ability),
                    cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<AbilityDTO>>(abilities);
        }
    }
}
