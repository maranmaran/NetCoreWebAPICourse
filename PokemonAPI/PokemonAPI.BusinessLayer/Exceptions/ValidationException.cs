﻿using PokemonAPI.DomainLayer.Interfaces;
using PokemonAPI.PersistenceLayer.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonAPI.BusinessLayer.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public ValidationException(string entityName, Dictionary<string, List<string>> errors, Exception ex = null)
            : base($"Message: Validation failed for {entityName}", ex)
        {
            _errors = errors;
        }
    }
}
