using MediatR;
using Microsoft.AspNetCore.Authorization;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.ProductPage.Queryes;

namespace ShopWave.Pages.OrderPage.Commands
{
    public record OrderProductCommand(Cart cart, byte deliveryprice) : IRequest<bool>;

    public class OrderProductHandler : IRequestHandler<OrderProductCommand, bool>
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        public OrderProductHandler(AppDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(OrderProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(request.cart.ProductId));
            var item = product.ProductVariations.FirstOrDefault(p => p.VariationId == request.cart.VariationId);
            if (product != null && item.quantity > 0)
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var order = new Order()
                        {
                            AppUserId = request.cart.AppUserId,
                            ProductId = request.cart.ProductId,
                            VariationId = request.cart.VariationId,
                            Date = DateTime.Now,
                            StatusId = 1,
                            TotalPrice = item.price + request.deliveryprice
                        };

                        await _context.Orders.AddAsync(order);
                        _context.Carts.Remove(request.cart);
                        await _context.SaveChangesAsync();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
