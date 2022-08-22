using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Dtos.AccountDtos
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string CheckPassword { get; set; }

    }
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.Username).NotEmpty();
            RuleFor(r => r.Fullname).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();
            RuleFor(r => r.CheckPassword).NotEmpty();
            RuleFor(r => r).Custom((r, context) =>
              {
                  if (r.Password!=r.CheckPassword)
                  {
                      context.AddFailure("CheckPassword", "wrong");
                  }
              });
        }
    }
}
