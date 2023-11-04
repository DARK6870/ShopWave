using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.OrderPage.Queryes
{
    public record GetAllOrdersQuery(string userId) : IRequest<List<Order>>;

    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<Order>>
    {
        private readonly AppDBContext _context;
        public GetAllOrdersHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            List<Order> res = await _context.Orders
                .Include(p => p.Statuses)
                .Include(p => p.Products)
                .Include(p => p.Products.ProductImages)
                .Include(p => p.Products.ProductVariations)
                .Where(p => p.AppUserId == request.userId)
                .OrderByDescending(p => p.Date)
                .ToListAsync();

            return res;
        }

    }
}
