using MediatR;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.CartPage.Commands
{
    public record AddItemToCartCommand(Cart cart) : IRequest<bool>;

    public class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand, bool>
    {
        private readonly AppDBContext _context;
        public AddItemToCartHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            var data = request.cart;

            await _context.Carts.AddAsync(data);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
