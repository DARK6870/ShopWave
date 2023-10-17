using MediatR;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.SupportPage.Commands
{
    public record AddSupportCommand(Support support) : IRequest<bool>;

    public class AddSupportHandler : IRequestHandler<AddSupportCommand, bool>
    {
        private readonly AppDBContext _context;
        public AddSupportHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddSupportCommand request, CancellationToken cancellationToken)
        {
            var sup = request.support;

            await _context.Supports.AddAsync(sup);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
