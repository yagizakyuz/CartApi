using FluentValidation;
using FluentValidation.Results;

namespace CartApi.Controllers.Requests.Validators
{
    public class RemoveCartItemRequestValidator : AbstractValidator<RemoveCartItemRequest>
    {
        protected override bool PreValidate(ValidationContext<RemoveCartItemRequest> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("RemoveCartItemRequestValidator", "Please ensure a model was supplied."));

                return false;
            }

            return true;
        }

        public RemoveCartItemRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
