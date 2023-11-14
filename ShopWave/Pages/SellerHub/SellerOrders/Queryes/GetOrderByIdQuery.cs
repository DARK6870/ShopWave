using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.AccountPage.Queryes;
using ShopWave.Pages.SellerHub.SellerOrders.ViewModels;

namespace ShopWave.Pages.SellerHub.SellerOrders.Queryes
{
    public record GetOrderByIdQuery(int id) : IRequest<SellerOrderViewModel>;

    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, SellerOrderViewModel>
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        public GetOrderByIdHandler(AppDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<SellerOrderViewModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            Order order = await _context.Orders
                            .Include(p => p.Statuses)
                            .Include(p => p.Products)
                            .Include(p => p.Products.ProductVariations)
                            .ThenInclude(p => p.Products.ProductImages)
                            .OrderByDescending(p => p.Date)
                            .FirstOrDefaultAsync(p => p.OrderId == request.id);

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
                Status = order.Statuses.StatusName,
                ImageData = order.Products.ProductImages.FirstOrDefault().Data
            };

            return orderViewModel;
        }
    }
}
