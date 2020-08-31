using Microsoft.EntityFrameworkCore;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.BusinessLayer.Models;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Implementations.DomainServices
{
    internal class BattleService : IBattleService
    {

        private readonly IRepository<Pokemon> _repository;

        public BattleService(IRepository<Pokemon> repository)
        {
            _repository = repository;
        }

        public async Task<BattleResult> Battle(Guid firstPokemonId, Guid secondPokemonId, CancellationToken cancellationToken = default)
        {
            var  firstPokemon = await _repository.Get(
                filter: dbPokemon => dbPokemon.Id == firstPokemonId,
                 include: source => source
                    .Include(x => x.Abilities)
                    .ThenInclude(x => x.Ability),
                cancellationToken: cancellationToken
            );

            var secondPokemon = await _repository.Get(
                filter: dbPokemon => dbPokemon.Id == secondPokemonId,
                 include: source => source
                    .Include(x => x.Abilities)
                    .ThenInclude(x => x.Ability),
                cancellationToken: cancellationToken
            );

            int GoesFirst(Pokemon firstPokemon, Pokemon secondPokemon) {
                if (firstPokemon.BaseStats.Speed > secondPokemon.BaseStats.Speed)
                {
                    return 1;
                }
                else if (firstPokemon.BaseStats.Speed < secondPokemon.BaseStats.Speed)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }

            int first = GoesFirst(firstPokemon, secondPokemon);
            int firstPokeHealth = firstPokemon.BaseStats.HealthPoints;
            int secondPokeHealth = secondPokemon.BaseStats.HealthPoints;
            bool specialFirst = false;
            bool specialSecond = false;

            do
            {
                if (first == 1)
                {
                    firstPokeHealth = firstPokemon.BaseStats.SpecialAttack - secondPokemon.BaseStats.SpecialDefense;
                    first = 2;
                    specialFirst = true;
                }
                else
                {
                    secondPokeHealth = secondPokemon.BaseStats.SpecialDefense - firstPokemon.BaseStats.SpecialAttack;
                    first = 1;
                    specialSecond = true;
                }
            }
            while (specialFirst && specialSecond && firstPokeHealth > 0 || secondPokeHealth > 0);

            do
            {
                if (first == 1)
                {   
                        firstPokeHealth = firstPokemon.BaseStats.Attack - secondPokemon.BaseStats.Defense;
                        first = 2;
                }
                else
                {
                        secondPokeHealth = secondPokemon.BaseStats.Defense - firstPokemon.BaseStats.Attack;
                        first = 1;
                }
                   
            }
            while (firstPokeHealth > 0 || secondPokeHealth > 0);
            
          
            if (firstPokemon.BaseStats.HealthPoints > secondPokemon.BaseStats.HealthPoints)
            {
                return new BattleResult(firstPokemon.Id, secondPokemon.Id);
            }
            else if (firstPokemon.BaseStats.HealthPoints < secondPokemon.BaseStats.HealthPoints)
            {
                return new BattleResult(secondPokemon.Id, firstPokemon.Id);
            }
            else
            {
                return null;
            }

            
        }
    }
}
