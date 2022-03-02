using FluentValidation;
using FluentValidation.Results;

namespace CartApi.Controllers.Requests.Validators
{
    public class AddCouponToCartRequestValidator : AbstractValidator<AddCouponToCartRequest>
    {
        protected override bool PreValidate(ValidationContext<AddCouponToCartRequest> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("AddCouponToCartRequestValidator", "Please ensure a model was supplied."));

                return false;
            }

            return true;
        }

        public AddCouponToCartRequestValidator()
        {
            RuleFor(x => x.CouponCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(1, 20);
        }
    }
}
