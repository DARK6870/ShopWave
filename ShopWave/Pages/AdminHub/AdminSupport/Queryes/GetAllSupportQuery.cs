using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AdminHub.AdminSupport.Queryes
{
    public record GetAllSupportQuery() : IRequest<List<Support>>;

    public class GetAllSupportHandler : IRequestHandler<GetAllSupportQuery, List<Support>>
    {
        private readonly AppDBContext _context;
        public GetAllSupportHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Support>> Handle(GetAllSupportQuery request, CancellationToken cancellationToken)
        {
            List<Support> supports = await _context.Supports.ToListAsync();

            return supports;
        }
    }
}
