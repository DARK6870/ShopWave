using MediatR;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AdminHub.AdminProduct.Commands
{
    public record DeclineProductCommand(int id) : IRequest<bool>;

    public class DeclineProductHandler : IRequestHandler<DeclineProductCommand, bool>
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        public DeclineProductHandler(AppDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeclineProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _context.Products.FindAsync(request.id);

            if (product != null)
            {
                _context.Products.Remove(product);
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
