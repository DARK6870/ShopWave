using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AccountPage.Commands
{
    public record ChangeUserDataCommand(UserData data) : IRequest<bool>;

    public class ChangeUserDataHandler : IRequestHandler<ChangeUserDataCommand, bool>
    {
        private readonly AppDBContext _context;
        public ChangeUserDataHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ChangeUserDataCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.UserDatas.FirstOrDefaultAsync(p => p.AppUserId == request.data.AppUserId);
            if (user != null)
            {
                bool dataChanged = user.PhoneNumber != request.data.PhoneNumber ||
                                  user.FirstName != request.data.FirstName ||
                                  user.LastName != request.data.LastName ||
                                  user.CountryId != request.data.CountryId ||
                                  user.Location != request.data.Location ||
                                  user.Address != request.data.Address ||
                                  user.PostalCode != request.data.PostalCode;

                if (dataChanged)
                {
                    user.PhoneNumber = request.data.PhoneNumber;
                    user.FirstName = request.data.FirstName;
                    user.LastName = request.data.LastName;
                    user.CountryId = request.data.CountryId;
                    user.Location = request.data.Location;
                    user.Address = request.data.Address;
                    user.PostalCode = request.data.PostalCode;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                await _context.UserDatas.AddAsync(request.data);
            }

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
