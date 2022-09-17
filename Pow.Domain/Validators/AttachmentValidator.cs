using FluentValidation;

namespace Pow.Domain.Validators
{
    public class AttachmentValidator : AbstractValidator<Attachment>
    {
        public AttachmentValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.File).NotNull();
            RuleFor(x => x.MessageId).NotNull();
        }
    }
}
