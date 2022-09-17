using FluentValidation;

namespace Pow.Domain.Validators
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Description).MaximumLength(10_000).WithMessage("Too long description.");
            RuleFor(x => x.Phone).Matches(@"^([\+]?33[-]?|[0])?[1-9][0-9]{8}$");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
