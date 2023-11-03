using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.ProductPage.Queryes
{
    public record GetOnlyProductByIdQuery(int id) : IRequest<Product>;

    public class GetOnlyProductByIdHandler : IRequestHandler<GetOnlyProductByIdQuery, Product>
    {
        private readonly AppDBContext _context;
        public GetOnlyProductByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(GetOnlyProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? result = await _context.Products.FindAsync(request.id);

            return result;
        }
    }
}
