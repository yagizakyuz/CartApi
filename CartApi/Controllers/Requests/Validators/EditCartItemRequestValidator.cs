using FluentValidation;
using FluentValidation.Results;

namespace CartApi.Controllers.Requests.Validators
{
    public class EditCartItemRequestValidator : AbstractValidator<EditCartItemRequest>
    {
        protected override bool PreValidate(ValidationContext<EditCartItemRequest> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("EditCartItemRequestValidator", "Please ensure a model was supplied."));

                return false;
            }

            return true;
        }

        public EditCartItemRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0);

            RuleFor(u => u.NewAmount)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .InclusiveBetween(1, 1000);
        }
    }
}
