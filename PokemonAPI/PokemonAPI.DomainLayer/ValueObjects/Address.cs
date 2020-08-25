using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonAPI.DomainLayer.ValueObjects
{
    public class Address : ValueObjectBase
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return City;
            yield return Street;
            yield return HouseNumber;
        }
    }
}
