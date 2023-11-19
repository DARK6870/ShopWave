using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Entity;
using ShopWave.Pages.AdminCategoryPage.Queryes;
using ShopWave.Pages.ProductPage.Queryes;
using ShopWave.Pages.ProductPage.ViewModels;
using ShopWave.Pages.SellerHub.SellerOrders.Commands;
using ShopWave.Pages.SellerHub.SellerOrders.Queryes;
using ShopWave.Pages.SellerHub.SellerOrders.ViewModels;
using ShopWave.Pages.SellerHub.SellerProduct.Commands;
using ShopWave.Pages.SellerHub.SellerProduct.Queryes;
using ShopWave.Pages.SellerHub.SellerProduct.ViewModels;

namespace ShopWave.Pages.SellerHub
{
    [Authorize(Roles = "Seller")]
    public class sellerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _usermanager;

        public sellerController(IMediator mediator, UserManager<IdentityUser> usermanager)
        {
            _mediator = mediator;
            _usermanager = usermanager;
        }

        public async Task<bool> validateSeller(string userId)
        {
            var currentuser = await _usermanager.GetUserAsync(User);
            bool res = currentuser.Id == userId;

            return res;
        }

        public IActionResult panel()
        {
            return View();
        }

        public async Task<IActionResult> myproducts()
        {
            try
            {
                var user = await _usermanager.GetUserAsync(User);
                List<Product> products = await _mediator.Send(new GetProductsBySellerIdQuery(user.Id));

                return View(products);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> manageOrders()
        {
            try
            {
                var user = await _usermanager.GetUserAsync(User);
                List<SellerOrderViewModel> orders = await _mediator.Send(new GetOrdersBySellerQuery(user.Id));
                return View(orders);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> orderhistory()
        {
            try
            {
                var user = await _usermanager.GetUserAsync(User);
                List<SellerOrderViewModel> orders = await _mediator.Send(new GetOrderHistoryBySellerQuery(user.Id));
                return View(orders);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> order(int id)
        {
            try
            {
                SellerOrderViewModel order_ = await _mediator.Send(new GetOrderByIdQuery(id));
                if (await validateSeller(order_.SellerId))
                {
                    OrderStatusViewModel model = new OrderStatusViewModel()
                    {
                        order = order_,
                        statuses = await _mediator.Send(new GetAllStatusesQuery())
                    };
                    return View(model);
                }
                else
                {
                    return Redirect("/Error");
                }
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> setstatus(int OrderId, byte StatusId)
        {
            try
            {
                Order order = await _mediator.Send(new GetOnlyOrderByIdQuery(OrderId));
                if (await validateSeller(order.Products.AppUserId))
                {
                    bool res = await _mediator.Send(new UpdateOrderCommand(OrderId, StatusId));
                    return Redirect("/seller/manageorders");
                }
                else
                {
                    return Redirect("/Error");
                }
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> addproduct()
        {
            try
            {
                NewProductViewModel model_ = new NewProductViewModel()
                {
                    categoryes = await _mediator.Send(new GetAllCategoryesQuery())
                };

                return View(model_);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(NewProductViewModel product)
        {
            try
            {
                if (product.Image.Length >= (1 * 1024 * 1024))
                {
                    TempData["Message"] = "The image size is too big, try compressing the image and try again.";
                    TempData["Icon"] = "info";
                    return Redirect("/seller/panel");
                }

                bool res = await _mediator.Send(new PostNewProductCommand(product));
                if (res)
                {
                    TempData["Message"] = "Product was added succesfull";
                    TempData["Icon"] = "success";
                    return Redirect("/seller/panel");
                }
                else
                {
                    TempData["Message"] = "Something went wrong.";
                    TempData["Icon"] = "error";
                    return Redirect("/seller/panel");
                }
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> editproduct(int id)
        {
            try
            {
                Product? product = await _mediator.Send(new GetOnlyProductByIdQuery(id));
                if (await validateSeller(product.AppUserId))
                {
                    EditProductViewModel model = new EditProductViewModel()
                    {
                        Name = product.ProductName,
                        Description = product.Description,
                        Id = product.ProductId,
                        CategoryId = product.CategoryId,
                        categories = await _mediator.Send(new GetAllCategoryesQuery())
                    };

                    return View(model);
                }
                else
                {
                    return Redirect("/Error");
                }
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> updateinfo(EditProductViewModel model)
        {
            try
            {
                Product? product = await _mediator.Send(new GetOnlyProductByIdQuery(model.Id));
                if (await validateSeller(product.AppUserId))
                {
                    if (await _mediator.Send(new UpdateProductInfoCommand(model)))
                    {
                        TempData["Icon"] = "success";
                        TempData["Message"] = "Product updated successfull.";
                    }
                    else
                    {
                        TempData["Icon"] = "error";
                        TempData["Message"] = "Something went wrong.";
                    }
                }
                return Redirect("/seller/panel");

            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> editimages(int id)
        {
            try
            {
                List<ProductImages> model = await _mediator.Send(new GetProductImagesQuery(id));
                return View(model);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> uploadnewimage(int ProductId, IFormFile ImageFile)
        {
            try
            {
                Product? product = await _mediator.Send(new GetOnlyProductByIdQuery(ProductId));
                if (await validateSeller(product.AppUserId))
                {
                    List<ProductImages> model = await _mediator.Send(new GetProductImagesQuery(ProductId));

                    if (model.Count() >= 8)
                    {
                        TempData["Message"] = "You cannot add more than 8 images to product";
                        TempData["Icon"] = "error";
                        return Redirect("/seller/panel");
                    }

                    if (ImageFile.Length >= (1 * 1024 * 1024))
                    {
                        TempData["Message"] = "The image size is too big, try compressing the image and try again.";
                        TempData["Icon"] = "info";
                        return Redirect("/seller/panel");
                    }

                    await _mediator.Send(new UploadNewImageCommand(ProductId, ImageFile));
                }

                return Redirect($"/seller/editimages/{ProductId}");
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> deleteimage(int id, int ProductId)
        {
            try
            {
                Product? product = await _mediator.Send(new GetOnlyProductByIdQuery(ProductId));
                if (await validateSeller(product.AppUserId))
                {
                    List<ProductImages>? model = await _mediator.Send(new GetProductImagesQuery(ProductId));

                    if (model.Count() == 1)
                    {
                        TempData["Message"] = "You cannot delete last product image.";
                        TempData["Icon"] = "error";
                        return Redirect("/seller/panel");
                    }

                    await _mediator.Send(new DeleteImageCommand(id));
                }
                return Redirect($"/seller/editimages/{ProductId}");
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> editvariations(int id)
        {
            try
            {
                List<ProductVariation> variations = await _mediator.Send(new GetProductVariationsQuery(id));
                return View(variations);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> addnewvariation(ProductVariation variation)
        {
            try
            {
                Product? product = await _mediator.Send(new GetOnlyProductByIdQuery(variation.ProductId));
                if (await validateSeller(product.AppUserId))
                {
                    await _mediator.Send(new PostNewVariationCommand(variation));
                }
                return Redirect($"/seller/editvariations/{variation.ProductId}");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
    }
}
