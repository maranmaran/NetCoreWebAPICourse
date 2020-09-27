using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.BusinessLayer.Validator;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests.BusinessLayer")]

namespace PokemonAPI.BusinessLayer.Implementations.DomainServices
{
    internal class TrainerService : ITrainerService
    {
        private readonly IRepository<Trainer> _repository;
        private readonly IMapper _mapper;
        private readonly TrainerValidator _validator = new TrainerValidator();
        private readonly ILogger<TrainerService> _logger;

        public TrainerService(IRepository<Trainer> repository, IMapper mapper, ILogger<TrainerService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Create(TrainerDTO trainer, CancellationToken cancellationToken = default)
        {

            _validator.Validate(trainer);
            _logger.LogInformation($"Validation for trainer: {trainer.Id} successfull");

            try
            {
                var trainerEntity = _mapper.Map<Trainer>(trainer);
                return await _repository.Insert(trainerEntity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _repository.Delete(id, cancellationToken);
                _logger.LogInformation($"Trainer with ID: {id} succesfully deleted)");
            }
            catch (Exception e)
            {
                throw new DeleteException(id, e);
            }
        }

        public async Task<TrainerDTO> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var trainer = await _repository.Get(
                filter: dbTrainer => dbTrainer.Id == id,
                include: src => src
                    .Include(t => t.CaughtPokemons),
                cancellationToken: cancellationToken
            );

            if (trainer == null)
                throw new NotFoundException(id);

            _logger.LogInformation($"Get request for trainer: {id} succesfull");
            return _mapper.Map<TrainerDTO>(trainer);
        }

        public async Task<IEnumerable<TrainerDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            var trainers = await _repository.GetAll(
                include: source => source
                    .Include(t => t.CaughtPokemons),
                cancellationToken: cancellationToken
            );

            _logger.LogInformation("Get all trainers request succesfull");
            return _mapper.Map<IEnumerable<TrainerDTO>>(trainers);
        }

        public async Task Update(TrainerDTO trainer, CancellationToken cancellationToken = default)
        {
            try
            {
                var trainerEntity = _mapper.Map<Trainer>(trainer);

                await _repository.Update(trainerEntity, cancellationToken);
                _logger.LogInformation($"Trainer with ID: {trainer.Id} succesfully updated");
            }
            catch (Exception e)
            {
                throw new UpdateException(trainer.Id, e);
            }
        }
    }
}
