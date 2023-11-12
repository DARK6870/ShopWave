using MediatR;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.AdminHub.AdminSeller.Queryes;

namespace ShopWave.Pages.AdminHub.AdminProduct.Commands
{
    public record AcceptProductCommand(int id) : IRequest<bool>;

    public class DeleteSellerDataHandler : IRequestHandler<AcceptProductCommand, bool>
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        public DeleteSellerDataHandler(AppDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AcceptProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _context.Products.FindAsync(request.id);

            if (product != null)
            {
                product.Admitered = true;
                int res = await _context.SaveChangesAsync();

                return res > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
