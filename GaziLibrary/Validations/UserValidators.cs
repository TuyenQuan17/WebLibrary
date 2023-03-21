using FluentValidation;
using GaziLibrary.Business.Concrete;
using GaziLibrary.DataAccess.Concrete;
using GaziLibrary.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaziLibrary.Validations
{
    public class UserValidators : AbstractValidator<User>
    {
        public UserValidators()
        {
            // Tên tài khoản
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Tên người dùng không được để trống.");
            RuleFor(u => u.UserName).GreaterThan("5").WithMessage("Tên người dùng phải có ít nhất 6 chữ số.");
            RuleFor(u => u.UserName).Must(UniqueUsername).WithMessage("Tên người dùng đã tồn tại.");

            //Tên đầu tiên
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("Tên không được để trống.");
            RuleFor(u => u.FirstName).MinimumLength(2).WithMessage("Tên phải chứa ít nhất 2 ký tự.");

            // Họ
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Họ không được để trống.");
            RuleFor(u => u.LastName).MinimumLength(2).WithMessage("Họ phải chứa ít nhất 2 ký tự.");

            // Mật khẩu
            RuleFor(u => u.Password).NotEmpty().WithMessage("Mật khẩu không được để trống.");
            RuleFor(u => u.Password).GreaterThan("5").WithMessage("Mật khẩu phải có ít nhất 6 chữ số.");

            // Thư điện tử
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email không được để trống.");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Nhập chính xác địa chỉ email của bạn.");
        }
        private bool UniqueUsername(string arg)
        {
            var userManager = new UserManager(new EfUserDal());
            return userManager.CheckUsername(arg).Success;
        }
    }
}
