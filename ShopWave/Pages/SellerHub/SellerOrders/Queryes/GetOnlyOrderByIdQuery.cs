using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.AccountPage.Queryes;
using ShopWave.Pages.SellerHub.SellerOrders.ViewModels;

namespace ShopWave.Pages.SellerHub.SellerOrders.Queryes
{
    public record GetOnlyOrderByIdQuery(int id) : IRequest<Order>;

    public class GetOnlyOrderByIdHandler : IRequestHandler<GetOnlyOrderByIdQuery, Order>
    {
        private readonly AppDBContext _context;
        public GetOnlyOrderByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Order> Handle(GetOnlyOrderByIdQuery request, CancellationToken cancellationToken)
        {
            Order order = await _context.Orders.Include(p => p.Products).FirstOrDefaultAsync(p => p.OrderId == request.id);

            return order;
        }
    }
}
