using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.CartPage.Queryes
{
    public record GetAllCartQuery(string userId) : IRequest<List<Cart>>;

    public class GetAllCartHandler : IRequestHandler<GetAllCartQuery, List<Cart>>
    {
        private readonly AppDBContext _context;
        public GetAllCartHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Cart>> Handle(GetAllCartQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Carts
                .Include(p => p.ProductVariations)
                .Include(p => p.Products)
                .ThenInclude(p => p.ProductImages)
                .Where(p => p.AppUserId == request.userId);

            return await result.ToListAsync();
        }

    }
}
