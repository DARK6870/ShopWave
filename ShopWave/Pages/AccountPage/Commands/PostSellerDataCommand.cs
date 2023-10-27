using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AccountPage.Commands
{
    public record PostSellerDataCommand(SellerData seller) : IRequest<bool>;

    public class PostSellerDataHandler : IRequestHandler<PostSellerDataCommand, bool>
    {
        private readonly AppDBContext _context;
        public PostSellerDataHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(PostSellerDataCommand request, CancellationToken cancellationToken)
        {
            await _context.SellerDatas.AddAsync(request.seller);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
