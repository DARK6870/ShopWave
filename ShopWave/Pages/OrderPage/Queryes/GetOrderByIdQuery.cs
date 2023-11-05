using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.OrderPage.Queryes
{
    public record GetOrderByIdQuery(int id) : IRequest<Order>;

    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly AppDBContext _context;
        public GetOrderByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            Order order = await _context.Orders.Include(p => p.Statuses).FirstOrDefaultAsync(p => p.OrderId == request.id);

            return order;
        }

    }
}
