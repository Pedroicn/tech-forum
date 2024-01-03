using FluentValidation;
using TechForum.Business.Models;

namespace TechForum.Api.Validations;

public class UserValidation : AbstractValidator<User>
{

  public UserValidation()
  {
    RuleFor(user => user.Name)
      .NotEmpty().WithMessage("The {PropertyName} field must be provided")
      .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} e {MaxLength}");
    
    RuleFor(user => user.Email)
      .NotEmpty().WithMessage("The {PropertyName} field must be provided")
      .EmailAddress().WithMessage("Type a valid email (test@gmail.com)");

    RuleFor(user => user.Password)
      .NotEmpty().WithMessage("The {PropertyName} field must be provided")
      .Length(8, 100).WithMessage("The {PropertyName} field must be between {MinLength} e {MaxLength}")
      .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
      .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
      .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
      .Matches("(?=.*[@#$%^&+=])").WithMessage("Your password must contain at least one non-alphanumeric symbol");
  }

}
