using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace OnlineShoppingStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1) {
            //we are sending a category parameter so we could not just view all records but we could also filter out our desired result in the url
            ProductsListViewModel model = new ProductsListViewModel //contains a list of Products and an instance of PagingInfo
            {
                Products = repository.Products    // the query for changing pages
                    .Where(p=>category == null || p.Category == category)  //could accept category being not sent or sent by the user
                    .OrderBy(p=>p.ProductID)
                    .Skip((page-1)*PageSize)      //the formula for making pages
                    .Take(PageSize),
                PagingInfo = new PagingInfo      //set the initial property values of PagingInfo
                {
                    CurrentPage = page,    //a page value that gets past to the url, initial value is 1 if nothing is passed
                    ItemsPerPage = PageSize, //value of PageSize is in this controller, default value is 4
                    TotalItems = category == null ?
                                 repository.Products.Count() : //if there are no category being sent by the user, just the total count
                                 repository.Products.Where(p=>p.Category==category).Count()  
                    //There is a missing property here that is TotalPages, in which it uses TotalItems and ItemsPerPage to determine it's value. It has no setter
                    //TotalPages is automatically set via TotalItems/ItemsPerPage                    
                },
                CurrentCategory = category   //value of category is inside route.config, this line is gonna use for the hyperlinks below the site
            };

            return View(model);
        }
    }
}