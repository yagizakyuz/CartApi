using Microsoft.AspNetCore.Mvc;
using CartApi.Controllers.Requests;
using CartApi.Controllers.Responses;
using CartApi.Data.Models;
using CartApi.Data.Enums;
using CartApi.Services.Interfaces;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("cart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IAuditLogService _auditLogService;


        public ShoppingCartController(IProductService productService,
                                      ICartService cartService, 
                                      IAuditLogService auditLogService)
        {
            _productService = productService;
            _cartService = cartService;
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            List<CartItem> items = await _cartService.GetCartItems();

            List<GetItemsResponse> responses = new List<GetItemsResponse>();

            foreach (CartItem item in items)
            {
                Product product = await _productService.GetProductById(item.ProductId);

                if(product == null)
                {
                    throw new Exception($"Get cart items api call failed. No product found with the productId: {item.ProductId}");
                }

                responses.Add(new GetItemsResponse
                {
                    Amount = item.Amount,
                    ProductId = item.ProductId,
                    ProductName = product.Name
                });
            }

            return Ok(items);
        }

        [HttpPost("items/add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            bool exists = await _productService.AnyProductById(request.ProductId);

            if (!exists)
            {
                return NotFound();
            }

            await _cartService.AddToCart(request);

            AddAuditLogRequest auditLog = new AddAuditLogRequest
            {
                Amount = request.Amount,
                ProductId = request.ProductId,
                ShoppingCartAction = ShoppingCartAction.Add
            };

            await _auditLogService.AddAuditLog(auditLog);

            return NoContent();
        }

        [HttpPut("items/edit")]
        public async Task<IActionResult> EditCartItem([FromBody] EditCartItemRequest request)
        {
            bool exists = await _productService.AnyProductById(request.ProductId);

            if (!exists)
            {
                return NotFound();
            }

            await _cartService.EditCart(request);

            AddAuditLogRequest auditLog = new AddAuditLogRequest
            {
                Amount = request.NewAmount,
                ProductId = request.ProductId,
                ShoppingCartAction = ShoppingCartAction.Edit
            };

            await _auditLogService.AddAuditLog(auditLog);

            return NoContent();
        }

        [HttpDelete("items/remove")]
        public async Task<IActionResult> RemoveCartItem([FromBody] RemoveCartItemRequest request)
        {
            await _cartService.RemoveFromCart(request);

            AddAuditLogRequest auditLog = new AddAuditLogRequest
            {
                Amount = 0,
                ProductId = request.ProductId,
                ShoppingCartAction = ShoppingCartAction.Remove
            };

            await _auditLogService.AddAuditLog(auditLog);


            return NoContent();
        }

        [HttpDelete("empty-cart")]
        public async Task<IActionResult> EmptyCart()
        {
            await _cartService.EmptyCart();

            AddAuditLogRequest auditLog = new AddAuditLogRequest
            {
                Amount = 0,
                ProductId = 0,
                ShoppingCartAction = ShoppingCartAction.Empty
            };

            await _auditLogService.AddAuditLog(auditLog);

            return NoContent();
        }

        [HttpPost("add-coupon")]
        public async Task<IActionResult> AddCouponToCart(AddCouponToCartRequest request)
        {
            bool success = await _cartService.AddCouponToCart(request);

            if (!success)
            {
                return NotFound("No coupon found with this coupon code.");
            }

            return NoContent();
        }

        [HttpDelete("remove-coupon")]
        public async Task<IActionResult> RemoveCouponFromCart()
        {
            await _cartService.RemoveCoupon();

            return NoContent();
        }

        [HttpGet("total-cost")]
        public async Task<IActionResult> GetTotalCost()
        {
            List<CartItem> items = await _cartService.GetCartItems();

            GetTotalCostResponse response = new GetTotalCostResponse();

            if (items.Count == 0)
            {
                return Ok(response);
            }

            foreach (CartItem item in items)
            {
                Product product = await _productService.GetProductById(item.ProductId);

                response.TotalCost += product.Price * item.Amount;

                response.TotalItem += item.Amount;
            }

            Coupon? coupon = await _cartService.GetCoupon();

            if (coupon != null)
            {
                response.NetCost = response.TotalCost * (100 - coupon.DiscountRate) / 100;
            }
            else
            {
                response.NetCost = response.TotalCost;
            }

            return Ok(response);
        }
    }
}