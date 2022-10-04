using FluentValidation;

namespace Pow.Domain.Validators
{
    public class MarkValidator : AbstractValidator<Mark>
    {
        public MarkValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.MapUrl).NotEmpty();
            RuleFor(x => x.GpsLongitude).NotEmpty().Matches(@"\d");
            RuleFor(x => x.GpsLatitude).NotEmpty().Matches(@"\d");
            /*RuleFor(x => x.UserId).NotNull();*/
        }
    }
}