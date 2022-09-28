using FluentValidation;

namespace Pow.Domain.Validators
{
    public class MarkValidator : AbstractValidator<Mark>
    {
        public MarkValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Region).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.StreetNumber).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.MapUrl).NotEmpty();
            RuleFor(x => x.GpsLongitude).NotEmpty().Matches(@"\d");
            RuleFor(x => x.GpsLatitude).NotEmpty().Matches(@"\d");
            /*RuleFor(x => x.UserId).NotNull();*/
        }
    }
}
