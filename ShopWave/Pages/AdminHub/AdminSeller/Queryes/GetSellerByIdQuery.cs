using MediatR;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AdminHub.AdminSeller.Queryes
{
    public record GetSellerByIdQuery(int id) : IRequest<SellerData>;

    public class GetSellerByIdHandler : IRequestHandler<GetSellerByIdQuery, SellerData>
    {
        private readonly AppDBContext _context;
        public GetSellerByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<SellerData> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
        {
            SellerData seller = await _context.SellerDatas.FindAsync(request.id);

            return seller;
        }
    }
}
