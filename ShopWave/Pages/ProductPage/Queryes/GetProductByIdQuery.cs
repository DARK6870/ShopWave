using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.AccountPage.Queryes;

namespace ShopWave.Pages.ProductPage.Queryes
{
    public record GetProductByIdQuery(int id) : IRequest<Product>;

    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        public GetProductByIdHandler(AppDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? result = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariations)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.ProductId == request.id);
            if (result != null)
            {
                foreach (var item in result.Reviews)
                {
                    item.AppUserId = await _mediator.Send(new GetUserAvatarByIdQuery(item.AppUserId));
                }
            }

            return result;
        }

    }
}
