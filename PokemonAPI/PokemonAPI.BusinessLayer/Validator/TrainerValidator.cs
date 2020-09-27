using FluentValidation;
using PokemonAPI.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonAPI.BusinessLayer.Validator
{
    public class TrainerValidator : AbstractValidator<TrainerDTO>
    {
		public TrainerValidator()
		{
			RuleFor(x => x.FullName).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("Trainer full name is required")
									.Must(fullName => IsNameAndSurname(fullName)).WithMessage("Full name must contain name and surname")
									.Must(fullName => AllLetters(fullName)).WithMessage("Full name must contain only letters");
		}

		public bool AllLetters(string fullName)
        {
			string[] splitedFullName = fullName.Split(' ');

			foreach (var name in splitedFullName)
            {
				if (!name.All(Char.IsLetter))
                {
					return false;
                }
            }
			return true;
		}

		public bool IsNameAndSurname(string fullName)
        {
			string[] splitedFullName = fullName.Split(' ');

			return splitedFullName.Length > 1 && splitedFullName.Length < 100;
            
		}
	}
}
