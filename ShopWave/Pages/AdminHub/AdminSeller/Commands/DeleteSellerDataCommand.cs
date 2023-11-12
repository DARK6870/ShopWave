using MediatR;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.AdminHub.AdminSeller.Queryes;

namespace ShopWave.Pages.AdminHub.AdminSeller.Commands
{
    public record DeleteSellerDataCommand(int id) : IRequest<bool>;

    public class DeleteSellerDataHandler : IRequestHandler<DeleteSellerDataCommand, bool>
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        public DeleteSellerDataHandler(AppDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeleteSellerDataCommand request, CancellationToken cancellationToken)
        {
            SellerData seller = await _mediator.Send(new GetSellerByIdQuery(request.id));
            if (seller != null)
            {
                _context.SellerDatas.Remove(seller);
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
