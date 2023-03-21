using FluentValidation;
using GaziLibrary.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaziLibrary.Validations
{
    public class PositionValidators : AbstractValidator<Position>
    {
        public PositionValidators()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Tên vị trí không được để trống.");
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("Tên vị trí phải chứa ít nhất 3 ký tự.");
        }
    }
}
