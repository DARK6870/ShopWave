using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AdminHub.AdminSeller.Queryes
{
    public record GetAllSellerRequestsQuery() : IRequest<List<SellerData>>;

    public class GetAllSellerRequestsHandler : IRequestHandler<GetAllSellerRequestsQuery, List<SellerData>>
    {
        private readonly AppDBContext _context;
        public GetAllSellerRequestsHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<SellerData>> Handle(GetAllSellerRequestsQuery request, CancellationToken cancellationToken)
        {
            List<SellerData> sellers = await _context.SellerDatas.ToListAsync();

            return sellers;
        }
    }
}
