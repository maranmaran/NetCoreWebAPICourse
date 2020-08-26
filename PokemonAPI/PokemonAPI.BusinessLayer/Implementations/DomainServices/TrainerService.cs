using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Implementations.DomainServices
{
    internal class TrainerService : ITrainerService
    {
        private readonly IRepository<Trainer> _repository;
        private readonly IMapper _mapper;

        public TrainerService(IRepository<Trainer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainerDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            var trainers = await _repository.GetAll(
                include: source => source
                    .Include(t => t.CaughtPokemons),
                cancellationToken: cancellationToken
            );

            return _mapper.Map<IEnumerable<TrainerDTO>>(trainers);
        }
    }
}
