using MediatR;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AdminHub.AdminSupport.Commands
{
    public record DeleteSupportCommand(int id) : IRequest<bool>;

    public class DeleteSupportHandler : IRequestHandler<DeleteSupportCommand, bool>
    {
        private readonly AppDBContext _context;
        public DeleteSupportHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteSupportCommand request, CancellationToken cancellationToken)
        {
            Support support = await _context.Supports.FindAsync(request.id);
            if (support == null)
            {
                return false;
            }

            _context.Remove(support);
            int res = await _context.SaveChangesAsync();
            return res > 0;
        }
    }
}
