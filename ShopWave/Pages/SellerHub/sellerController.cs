﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Entity;
using ShopWave.Pages.AdminCategoryPage.Queryes;
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
    }
}