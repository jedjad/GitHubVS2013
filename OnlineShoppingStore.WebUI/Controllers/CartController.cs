using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Entities;
using OnlineShoppingStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.WebUI.Controllers
{

    public class CartController : Controller
    {

        private IProductRepository repository;     //collection of products

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }


        public ViewResult Index(string returnUrl)
        {
            return View(
                new CartIndexViewModel { Cart = GetCart(), ReturnUrl = returnUrl }); //this are the properties of CartIndexViewModel
        }
        
        

        public RedirectToRouteResult AddToCart(int productId, string returnUrl)  //recieving from ProductSummary View
        {
      
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);  //search a product from a collection(of products) via recieved productId
            //repository = IProductRespository = collection of Products where Products has ProductID property
            if (product != null)   //if product is existed(this system is just for foolproof), we have to add the product to our cart
            {
                GetCart().AddItem(product, 1);  //AddItem is a method of Cart class, add item into the new cart
                //AddItem method recieves a product object and adds a new Item in the list of ordered goods, one at a time every click, that's why it's 1
            }
            
            return RedirectToAction("Index", new {returnUrl});
        }


        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)   
            {
                GetCart().RemoveLine(product);
            }
            
            return RedirectToAction("Index", new {returnUrl});
        }


        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];   // cart = new Cart(); if it is not null
            if (cart == null)
            {
                cart = new Cart();         //make a new cart object
                Session["Cart"] = cart;    //store the new cart into session, a Session could store any type of object
                //Session variables are stored on the web server by default, and are kept for a lifetime of a session
                //available across all pages, but only for a single session
                //like single-user global data
            }
            return cart;
        }
        
    }
}