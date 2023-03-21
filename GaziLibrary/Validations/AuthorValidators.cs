using FluentValidation;
using GaziLibrary.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaziLibrary.Validations
{
    public class AuthorValidators : AbstractValidator<Author>
    {
        public AuthorValidators()
        {
            RuleFor(a => a.FirstName).NotEmpty().WithMessage("Tên tác giả không được để trống.");
            RuleFor(a => a.FirstName).MinimumLength(2).WithMessage("Tên tác giả phải chứa ít nhất 2 ký tự.");

            RuleFor(a => a.LastName).NotEmpty().WithMessage("Họ tác giả không được để trống.");
            RuleFor(a => a.LastName).MinimumLength(2).WithMessage("Họ tác giả phải chứa ít nhất 2 ký tự.");
        }
    }
}
