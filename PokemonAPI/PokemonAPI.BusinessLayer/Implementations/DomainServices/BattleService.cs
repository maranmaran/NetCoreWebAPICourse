using Microsoft.EntityFrameworkCore;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.BusinessLayer.Models;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions.Impl;
using PokemonAPI.PersistenceLayer.DTOModels;

[assembly: InternalsVisibleTo("Tests.BusinessLayer")]
namespace PokemonAPI.BusinessLayer.Implementations.DomainServices
{
    internal class BattleService : IBattleService
    {

        private readonly IPokemonService _pokemonService;

        public BattleService(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        public async Task<BattleResult> Battle(Guid firstPokemonId, Guid secondPokemonId, CancellationToken cancellationToken = default)
        {
            // get pokemon data
            var firstPokemon = await _pokemonService.Get(firstPokemonId, cancellationToken);
            var secondPokemon = await _pokemonService.Get(secondPokemonId, cancellationToken);
            
            // Determine attack for turn based battle
            var attackerId = GetFirstAttacker(firstPokemon, secondPokemon);
 
            // Do special attack and special defense moves first - Get remaining health
            firstPokemon.BaseStats.HealthPoints = firstPokemon.BaseStats.HealthPoints + firstPokemon.BaseStats.SpecialDefense - secondPokemon.BaseStats.SpecialAttack;
            secondPokemon.BaseStats.HealthPoints = secondPokemon.BaseStats.HealthPoints + secondPokemon.BaseStats.SpecialDefense - firstPokemon.BaseStats.SpecialAttack;

            if (firstPokemon.BaseStats.HealthPoints < 0 || secondPokemon.BaseStats.HealthPoints < 0)
            {
                return GetBattleResult(firstPokemon, secondPokemon);
            }

            DoBattle(attackerId, firstPokemon, secondPokemon);

            return GetBattleResult(firstPokemon, secondPokemon);
        }

        private void DoBattle(Guid attackerId, PokemonDTO firstPokemon, PokemonDTO secondPokemon)
        {
            // Instantiate queue for battle
            var queue = GetPokemonQueueForBattling(attackerId, firstPokemon, secondPokemon);

            // Do battle
            while (firstPokemon.BaseStats.HealthPoints > 0 || secondPokemon.BaseStats.HealthPoints > 0)
            {
                var currentAttackingPokemon = queue.Dequeue();
                var currentDefensivePokemon = queue.Dequeue();

                // do attack
                currentDefensivePokemon.BaseStats.HealthPoints -= currentAttackingPokemon.BaseStats.Attack;

                queue.Enqueue(currentDefensivePokemon);
                queue.Enqueue(currentAttackingPokemon);
            }
        }

        internal BattleResult GetBattleResult(PokemonDTO firstPokemon, PokemonDTO secondPokemon)
        {
            // return results
            if (firstPokemon.BaseStats.HealthPoints > secondPokemon.BaseStats.HealthPoints)
            {
                return new BattleResult(firstPokemon.Id, secondPokemon.Id);
            }

            return new BattleResult(secondPokemon.Id, firstPokemon.Id);
        }

        internal Queue<PokemonDTO> GetPokemonQueueForBattling(Guid attackerId, PokemonDTO firstPokemon,
            PokemonDTO secondPokemon)
        {
            var queue = new Queue<PokemonDTO>();
            if (attackerId == firstPokemon.Id)
            {
                queue.Enqueue(firstPokemon);
                queue.Enqueue(secondPokemon);
            }
            else
            {
                queue.Enqueue(secondPokemon);
                queue.Enqueue(firstPokemon);
            }

            return queue;
        }

        /// <summary>
        /// Returns first attacking pokemon out of two provided as parameters
        /// Who goes first is determined by speed
        /// </summary>
        internal Guid GetFirstAttacker(PokemonDTO firstPokemon, PokemonDTO secondPokemon)
        {
            if (firstPokemon.BaseStats.Speed >= secondPokemon.BaseStats.Speed)
            {
                return firstPokemon.Id;
            }

            return secondPokemon.Id;
        }
    }
}
