using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaziLibrary.Validations
{
    public class TypeValidators : AbstractValidator<Entities.Concrete.Type>
    {
        public TypeValidators()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("Tên loại không được để trống.");
            RuleFor(t => t.Name).MinimumLength(2).WithMessage("Tên loại phải chứa ít nhất 2 ký tự.");
        }
    }
}
