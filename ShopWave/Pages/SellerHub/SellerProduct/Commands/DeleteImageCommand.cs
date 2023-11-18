using MediatR;
using ShopWave.Context;
using ShopWave.Entity;
using Z.EntityFramework.Plus;

namespace ShopWave.Pages.SellerHub.SellerProduct.Commands
{
    public record DeleteImageCommand(int id) : IRequest<bool>;

    public class DeleteImageHandler : IRequestHandler<DeleteImageCommand, bool>
    {
        private readonly AppDBContext _context;
        public DeleteImageHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            ProductImages? image = await _context.ProductImages.FindAsync(request.id);
            if (image == null)
            {
                return false;
            }

            _context.ProductImages.Remove(image);
            int rows = await _context.SaveChangesAsync();

            return rows > 0;
        }
    }
}
