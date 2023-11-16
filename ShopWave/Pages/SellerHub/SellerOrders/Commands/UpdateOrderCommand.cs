using MediatR;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.AccountPage.Queryes;
using ShopWave.Pages.SellerHub.SellerOrders.ViewModels;

namespace ShopWave.Pages.SellerHub.SellerOrders.Commands
{
    public record UpdateOrderCommand(int OrderId, byte StatusId) : IRequest<bool>;

    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly AppDBContext _context;
        public UpdateOrderHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = await _context.Orders.FindAsync(request.OrderId);
            if (order.StatusId != 5 && order.StatusId != 6)
            {
                order.StatusId = request.StatusId;

                int res = await _context.SaveChangesAsync();

                return res > 0;
            }
            else return false;
        }
    }
}
