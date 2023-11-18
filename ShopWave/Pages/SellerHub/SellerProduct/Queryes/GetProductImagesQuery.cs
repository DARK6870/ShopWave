using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.SellerHub.SellerProduct.Queryes
{
    public record GetProductImagesQuery(int id) : IRequest<List<ProductImages>>;

    public class GetProductImagesHandler : IRequestHandler<GetProductImagesQuery, List<ProductImages>>
    {
        private readonly AppDBContext _context;
        public GetProductImagesHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductImages>> Handle(GetProductImagesQuery request, CancellationToken cancellationToken)
        {
            List<ProductImages> images = await _context.ProductImages.Where(p => p.ProductId == request.id).ToListAsync();

            return images;
        }
    }
}
