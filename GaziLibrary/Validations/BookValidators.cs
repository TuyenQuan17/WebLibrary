using FluentValidation;
using GaziLibrary.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaziLibrary.Validations
{
    public class BookValidators : AbstractValidator<Book>
    {
        public BookValidators()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Tên sách không được để trống.");
            RuleFor(b => b.Name).MinimumLength(2).WithMessage("Tên sách phải chứa ít nhất 2 ký tự.");

            RuleFor(b => b.NumberOfPage).NotEmpty().WithMessage("Số trang không được để trống.");
            RuleFor(b => b.NumberOfPage).LessThanOrEqualTo(5000).WithMessage("Số trang không được lớn hơn 5000.");
        }
    }
}
