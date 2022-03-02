using CartApi.Contants;
using CartApi.Controllers.Requests;
using CartApi.Data.Models;
using CartApi.Services.Interfaces;

namespace CartApi.Services
{
    public class CartService : ICartService
    {
        private static ShoppingCart _cart = new ShoppingCart { Id = 1 };

        public async Task<List<CartItem>> GetCartItems()
        {
            return _cart.Items;
        }

        public async Task AddToCart(AddToCartRequest request)
        {
            CartItem? cartItem = _cart.Items.FirstOrDefault(x => x.ProductId == request.ProductId);

            if (cartItem == null)
            {
                _cart.Items.Add(new CartItem { Amount = request.Amount, ProductId = request.ProductId });
            }
            else
            {
                cartItem.Amount += request.Amount;

                if (cartItem.Amount > 1000)
                {
                    cartItem.Amount = 1000;
                }
            }
        }

        public async Task EditCart(EditCartItemRequest request)
        {
            CartItem? cartItem = _cart.Items.FirstOrDefault(x => x.ProductId == request.ProductId);

            if (cartItem == null)
            {
                throw new Exception($"Cart item does not exist with this product id: {request.ProductId}");
            }

            cartItem.Amount = request.NewAmount;
        }

        public async Task RemoveFromCart(RemoveCartItemRequest request)
        {
            int index = _cart.Items.FindIndex(x => x.ProductId == request.ProductId);

            if (index != -1)
            {
                _cart.Items.RemoveAt(index);
            }
        }

        public async Task EmptyCart()
        {
            _cart.Items = new List<CartItem>();
        }

        public async Task<bool> AddCouponToCart(AddCouponToCartRequest request)
        {
            Coupon? coupon;

            if (Constants.ValidCoupons.TryGetValue(request.CouponCode, out coupon))
            {
                _cart.Coupon = coupon;
                return true;
            }

            return false;
        }

        public async Task RemoveCoupon()
        {
            _cart.Coupon = null;
        }

        public async Task<Coupon?> GetCoupon()
        {
            return _cart.Coupon;
        }
    }
}
