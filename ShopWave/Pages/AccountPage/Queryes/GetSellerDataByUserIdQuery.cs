using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AccountPage.Queryes
{
    public record GetSellerDataByUserIdQuery(string id) : IRequest<SellerData>;
    public class GetSellerDataByUserIdHandler : IRequestHandler<GetSellerDataByUserIdQuery, SellerData>
    {
        private readonly AppDBContext _context;
        public GetSellerDataByUserIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<SellerData?> Handle(GetSellerDataByUserIdQuery request, CancellationToken cancellationToken)
        {
            SellerData? seller = await _context.SellerDatas.FirstOrDefaultAsync(p => p.AppUserId == request.id);
            return seller;
        }
    }
}
