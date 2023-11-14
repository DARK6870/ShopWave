using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Migrations;
using ShopWave.Pages.AccountPage.Queryes;
using ShopWave.Pages.OrderPage.ViewModels;
using ShopWave.Pages.SellerHub.SellerOrders.ViewModels;

namespace ShopWave.Pages.SellerHub.SellerOrders.Queryes
{
    public record GetOrdersBySellerQuery(string userId) : IRequest<List<SellerOrderViewModel>>;

    public class GetOrdersBySellerHandler : IRequestHandler<GetOrdersBySellerQuery, List<SellerOrderViewModel>>
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        public GetOrdersBySellerHandler(AppDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<List<SellerOrderViewModel>> Handle(GetOrdersBySellerQuery request, CancellationToken cancellationToken)
        {
            List<Order> orders = await _context.Orders
                            .Include(p => p.Statuses)
                            .Include(p => p.Products)
                            .Include(p => p.Products.ProductVariations)
                            .Where(p => p.Products.AppUserId == request.userId)
                            .OrderByDescending(p => p.Date)
                            .ToListAsync();

            List<SellerOrderViewModel> orderViewModels = new List<SellerOrderViewModel>();

            foreach (var order in orders)
            {
                UserData user = await _mediator.Send(new GetPostalDataByIdQuery(order.AppUserId));

                SellerOrderViewModel orderViewModel = new SellerOrderViewModel()
                {
                    ID = order.OrderId,
                    ProductName = order.Products.ProductName,
                    VariationName = order.ProductVariations.VariationName,
                    totalPrice = order.TotalPrice,
                    Address = user.Address,
                    PostalCode = user.PostalCode,
                    CountryName = user.Countryess.CountryName,
                    Phone = user.PhoneNumber,
                    Status = order.Statuses.StatusName
                };

                orderViewModels.Add(orderViewModel);
            }

            return orderViewModels;
        }
    }
}
