using FluentValidation;

namespace QAEngine.Domain.Resources
{
    public class QuestionCreateRequestValidator : AbstractValidator<QuestionCreateRequest>
    {
        public QuestionCreateRequestValidator()
        {
            this.RuleFor(q => q.Content)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
