using LazyCache;
using MediatR;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.ProductPage.Queryes
{
    public record GetOrderCountByProductIdQuery(int id) : IRequest<int>;

    public class GetOrderCountByProductIdHandler : IRequestHandler<GetOrderCountByProductIdQuery, int>
    {
        private readonly AppDBContext _context;
        public GetOrderCountByProductIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetOrderCountByProductIdQuery request, CancellationToken cancellationToken)
        {
            int count = _context.Orders.Where(p => p.ProductId == request.id).Count();

            return count;
        }
    }
}
