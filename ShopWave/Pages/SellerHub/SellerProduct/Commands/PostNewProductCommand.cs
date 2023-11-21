using MediatR;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.SellerHub.SellerProduct.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace ShopWave.Pages.SellerHub.SellerProduct.Commands
{
    public record PostNewProductCommand(NewProductViewModel newproduct) : IRequest<bool>;

    public class PostNewProductHandler : IRequestHandler<PostNewProductCommand, bool>
    {
        private readonly AppDBContext _context;
        public PostNewProductHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(PostNewProductCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Product product = new Product()
                    {
                        ProductDate = DateTime.Now,
                        ProductName = request.newproduct.ProductName,
                        Description = request.newproduct.ProductDescription,
                        AvgStars = 0,
                        OrderCounts = 0,
                        Admitered = false,
                        CategoryId = request.newproduct.CategoryId,
                        AppUserId = request.newproduct.SellerId
                    };

                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();

                    ProductVariation variation = new ProductVariation()
                    {
                        ProductId = product.ProductId,
                        VariationName = request.newproduct.VariationName,
                        quantity = request.newproduct.Quantity,
                        price = request.newproduct.Price
                    };

                    await _context.ProductVariations.AddAsync(variation);

                    ProductImages imges = new ProductImages()
                    {
                        ProductId = product.ProductId
                    };

                    if (request.newproduct.Image != null && request.newproduct.Image.Length > 0)
                    {
                        var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".webp" };
                        var fileExtension = Path.GetExtension(request.newproduct.Image.FileName);

                        if (allowedExtensions.Contains(fileExtension.ToLower()))
                        {
                            using (var stream = new MemoryStream())
                            {
                                await request.newproduct.Image.CopyToAsync(stream);
                                imges.Data = Convert.ToBase64String(stream.ToArray());
                            }
                        }
                    }

                    await _context.ProductImages.AddAsync(imges);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
