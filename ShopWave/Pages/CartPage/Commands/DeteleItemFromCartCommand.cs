using MediatR;
using ShopWave.Context;
using Microsoft.Identity;
using ShopWave.Entity;

namespace ShopWave.Pages.CartPage.Commands
{
    public record DeteleItemFromCartCommand(int id, string userId) : IRequest<bool>;

    public class DeteleItemFromCartHandler : IRequestHandler<DeteleItemFromCartCommand, bool>
    {
        private readonly AppDBContext _context;
        public DeteleItemFromCartHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeteleItemFromCartCommand request, CancellationToken cancellationToken)
        {
            int result = 0;

            var cartData = await _context.Carts.FindAsync(request.id);

            if (cartData != null)
            {
                if(cartData.AppUserId == request.userId)
                {
                 _context.Remove(cartData);
                 result = await _context.SaveChangesAsync();
                }
            }
            
            return result > 0;
        }
    }
}
