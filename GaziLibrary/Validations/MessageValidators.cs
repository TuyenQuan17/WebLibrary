using FluentValidation;
using GaziLibrary.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaziLibrary.Validations
{
    public class MessageValidators : AbstractValidator<Message>
    {
        public MessageValidators()
        {
            RuleFor(m => m.Text).NotEmpty().WithMessage("Phần tin nhắn không được để trống.");
            RuleFor(m => m.Text).MinimumLength(30).WithMessage("Phần tin nhắn phải chứa ít nhất 30 ký tự.");
            RuleFor(m => m.Text).MaximumLength(500).WithMessage("Bạn có thể nhập tối đa 500 ký tự.");
        }
    }
}
