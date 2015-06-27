using OnlineShoppingStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers   // the class that holds an extension method needs to be static
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)  // this is called an extension method for HtmlHelper
        {                                   //e.q. HtmlHelper.PageLinks
                                            //Html.PageLinks(Model.PagingInfo, x => Url.Action("List", new { page = x, category=Model.CurrentCategory }))
                                            //pagingInfo = Model.PagingInfo
                                            //Func<int,string> pageUrl = x=>Url.Action("List", new {page=x, category=Model.CurrentCategory})) 
            
            StringBuilder result = new StringBuilder();      //this will hold our links
            for (int i = 1; i <= pagingInfo.TotalPages; i++)  //generate individual tags for different links, pagingInfo is nothing more but a placeholder to the amount of total pages needed
            {
                TagBuilder tag = new TagBuilder("a");//anchor tag, create a new link every iteration
                tag.MergeAttribute("href", pageUrl(i)); 
                tag.InnerHtml = i.ToString();  //place a number in between the opening and closing tags of the element
                
                //this part is nothing but making a design on the current selected page
                if (i == pagingInfo.CurrentPage) //if i is the current page
                {
                    tag.AddCssClass("Selected");       //add some classes to the anchor tag
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());  //concatinate the Tagbuilder 'tag' to 'result'
            }
            return MvcHtmlString.Create(result.ToString());  //ceate and return the Html String 'result', contains a string of multiple links
        }
    }
}