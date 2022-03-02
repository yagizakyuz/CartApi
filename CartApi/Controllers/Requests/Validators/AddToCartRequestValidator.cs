using FluentValidation;
using FluentValidation.Results;

namespace CartApi.Controllers.Requests.Validators
{
    public class AddToCartRequestValidator : AbstractValidator<AddToCartRequest>
    {
        protected override bool PreValidate(ValidationContext<AddToCartRequest> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("AddToCartRequestValidator", "Please ensure a model was supplied."));

                return false;
            }

            return true;
        }

        public AddToCartRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0);

            RuleFor(u => u.Amount)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .InclusiveBetween(1, 1000);
        }
    }
}
