using MediatR;
using Microsoft.Identity.Client;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.OrderPage.Queryes;
using ShopWave.Pages.ProductPage.Queryes;

namespace ShopWave.Pages.OrderPage.Commands
{
    public record PostReviewCommand(int orderId, byte stars, string text) : IRequest<bool>;

    public class PostReviewHandler : IRequestHandler<PostReviewCommand, bool>
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        public PostReviewHandler(AppDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(PostReviewCommand request, CancellationToken cancellationToken)
        {
            Order order = await _mediator.Send(new GetOrderByIdQuery(request.orderId));
            
            if (order != null)
            {
                Review review = new Review()
                {
                    AppUserId = order.AppUserId,
                    ProductId = order.ProductId,
                    NumOfStarts = request.stars,
                    ReviewText = request.text
                };

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                    order.StatusId = 6;
                    await _context.Reviews.AddAsync(review);
                    int res = await _context.SaveChangesAsync();

                    transaction.Commit();

                    return res > 0;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
