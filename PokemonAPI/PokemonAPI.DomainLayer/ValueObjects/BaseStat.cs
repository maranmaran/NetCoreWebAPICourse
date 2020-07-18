using System.Collections.Generic;

namespace PokemonAPI.DomainLayer.ValueObjects
{
    public class BaseStat : ValueObjectBase
    {
        public int HealthPoints { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }


        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return HealthPoints;
            yield return Attack;
            yield return Defense;
            yield return SpecialAttack;
            yield return SpecialDefense;
            yield return Speed;
        }
    }
}