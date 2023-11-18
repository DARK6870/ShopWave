using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.ProductPage.Queryes;
using ShopWave.Pages.SellerHub.SellerOrders.Queryes;
using ShopWave.Pages.SellerHub.SellerProduct.ViewModels;

namespace ShopWave.Pages.SellerHub.SellerProduct.Commands
{
    public record UpdateProductInfoCommand(EditProductViewModel updated) : IRequest<bool>;

    public class UpdateProductInfoHandler : IRequestHandler<UpdateProductInfoCommand, bool>
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        public UpdateProductInfoHandler(AppDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }


        public async Task<bool> Handle(UpdateProductInfoCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _mediator.Send(new GetOnlyProductByIdQuery(request.updated.Id));
            if (product == null)
            {
                return false;
            }

            product.ProductName = request.updated.Name;
            product.Description = request.updated.Description;
            product.CategoryId = request.updated.CategoryId;

            int res = await _context.SaveChangesAsync();

            return res > 0;
        }
    }
}
