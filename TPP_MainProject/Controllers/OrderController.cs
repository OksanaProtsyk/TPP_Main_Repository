using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPP_MainProject.Models;
using TPP_MainProject.Models.entities;
using TPP_MainProject.Models.repository;
using TPP_MainProject.Models.ViewModels;

namespace TPP_MainProject.Controllers
{
    public class OrderController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Order
       public ActionResult Items()
        {
            IEnumerable<ProductItem> items = unitOfWork.ProductItemRepository.Get().ToList<ProductItem>();
            return View(items);
        }
        
    


        //
        // GET: /Order/
        public ActionResult Index()
        {
            var cart = OrderCart.GetCart(this.HttpContext);
 
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Order/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var addedAlbum = unitOfWork.ProductItemRepository.dbSet
                .Single(album => album.ID == id);
 
            // Add it to the shopping cart
            var cart = OrderCart.GetCart(this.HttpContext);
 
            cart.AddToCart(addedAlbum);
 
            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Order order)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.OrderRepository.Insert(order);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(order);
        }
        //
        // AJAX: /Order/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = OrderCart.GetCart(this.HttpContext);
 
            // Get the name of the album to display confirmation
            string albumName = unitOfWork.CartRepository.dbSet
                .Single(item => item.RecordId == id).ProductItem.Name;
 
            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);
 
            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /Order/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = OrderCart.GetCart(this.HttpContext);
 
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }

}