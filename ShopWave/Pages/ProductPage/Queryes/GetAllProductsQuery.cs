﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.ProductPage.Queryes
{
    public record GetAllProductsQuery() : IRequest<List<Product>>;

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly AppDBContext _context;
        public GetAllProductsHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Include(p => p.Categoriess) // Include Categories navigation property
                .Include(p => p.Statuses) // Include Statuses navigation property
                .ToListAsync();

            // Fetch ProductIds
            var productIds = products.Select(p => p.ProductId).ToList();

            // Fetch ProductImages and Images using Join and Select
            var productImages = await _context.ProductImages
                .Where(pi => productIds.Contains(pi.ProductId))
                .Include(pi => pi.Images) // Include Images navigation property
                .ToListAsync();

            // Attach ProductImages and Images to their respective Products
            foreach (var product in products)
            {
                product.ProductImages = productImages.Where(pi => pi.ProductId == product.ProductId).ToList();
            }

            return products;
        }
    }
}