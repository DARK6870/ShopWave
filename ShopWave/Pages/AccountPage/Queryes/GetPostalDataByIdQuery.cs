using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AccountPage.Queryes
{
    public record GetPostalDataByIdQuery(string id) : IRequest<UserData>;

    public class GetPostalDataByIdHandler : IRequestHandler<GetPostalDataByIdQuery, UserData>
    {
        private readonly AppDBContext _context;
        public GetPostalDataByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<UserData> Handle(GetPostalDataByIdQuery request, CancellationToken cancellationToken)
        {
            UserData? user = await _context.UserDatas.Include(p => p.Countryess).FirstOrDefaultAsync(p => p.AppUserId == request.id);

            return user;
        }
    }
}
