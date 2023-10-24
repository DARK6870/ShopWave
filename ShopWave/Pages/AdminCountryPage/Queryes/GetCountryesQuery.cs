using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AdminCountryPage.Queryes
{
    public record GetCountryesQuery() : IRequest<List<Countryes>>;

    public class GetCountryesHnadler : IRequestHandler<GetCountryesQuery, List<Countryes>>
    {
        private readonly AppDBContext _context;
        private readonly IAppCache _cache;
        public GetCountryesHnadler(AppDBContext context, IAppCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<Countryes>> Handle(GetCountryesQuery request, CancellationToken cancellationToken)
        {
            var countrydata = await _context.Countryes.ToListAsync();

            return countrydata;
        }
    }
}
