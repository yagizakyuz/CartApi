using CartApi.Controllers.Requests;
using CartApi.Data.Models;

namespace CartApi.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartItem>> GetCartItems();

        Task AddToCart(AddToCartRequest request);

        Task EditCart(EditCartItemRequest request);

        Task RemoveFromCart(RemoveCartItemRequest request);

        Task EmptyCart();

        Task<bool> AddCouponToCart(AddCouponToCartRequest request);

        Task RemoveCoupon();

        Task<Coupon?> GetCoupon();
    }
}
