﻿using AutoMapper;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.DTOModels;
using System.Linq;

namespace PokemonAPI.BusinessLayer
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            // map between pokemon entities
            // maps same name properties
            CreateMap<Pokemon, PokemonDTO>()
                .ReverseMap()
                // special handling for abilities because they have many2many join entity
                .ForMember(d => d.Abilities,
                    opt => opt.MapFrom(
                        // for each pokemon abilityDTO map to PokemonAbility join entity
                        p => p.Abilities
                            .Select(a => new PokemonAbility
                            {
                                PokemonId = p.Id,
                                AbilityId = a.Id,
                                Ability = new Ability()
                                {
                                    Id = a.Id,
                                    Description = a.Description,
                                    Name = a.Name,
                                }
                            })
                    )
                );

            // Turn pokemon abilities join entity into abilityDTO
            // map join entity between pokemon and it's abilities to abilities DTO
            // to flatten it
            CreateMap<PokemonAbility, AbilityDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AbilityId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Ability.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Ability.Description));


        }
    }
}
