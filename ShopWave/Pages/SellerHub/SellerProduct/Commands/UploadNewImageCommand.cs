using MediatR;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.SellerHub.SellerProduct.Commands
{
    public record UploadNewImageCommand(int ProductId, IFormFile Image) : IRequest<bool>;

    public class UploadNewImageHandler : IRequestHandler<UploadNewImageCommand, bool>
    {
        private readonly AppDBContext _context;
        public UploadNewImageHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UploadNewImageCommand request, CancellationToken cancellationToken)
        {
            if (request.Image != null && request.Image.Length > 0)
            {
                ProductImages productImages = new ProductImages()
                {
                    ProductId = request.ProductId
                };

                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                var fileExtension = Path.GetExtension(request.Image.FileName);

                if (allowedExtensions.Contains(fileExtension.ToLower()))
                {
                    using (var stream = new MemoryStream())
                    {
                        await request.Image.CopyToAsync(stream);
                        productImages.Data = Convert.ToBase64String(stream.ToArray());

                        await _context.ProductImages.AddAsync(productImages);
                        int rows = await _context.SaveChangesAsync();

                        return rows > 0;
                    }
                }
            }

            return false;
        }
    }
}
