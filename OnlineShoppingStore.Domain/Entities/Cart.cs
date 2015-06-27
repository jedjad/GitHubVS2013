using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingStore.Domain.Entities
{

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>(); //Cartline has an instance of Product and a Quantity

        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.Where(p => p.Product == product).FirstOrDefault(); //return a product or null
            if (line == null)
            {
                lineCollection.Add(
                    new CartLine { Product = product, Quantity = quantity });   // add an order if there is no order
            }
            else   //if there is already a pre-exisitng order, program assumes we are just adding additional quantity
            {
                line.Quantity += quantity;  //just add the amount of order to the existing order, line is a product query
            }
        }

        public void RemoveLine(Product product) //remove a product from the order
        {
            lineCollection.RemoveAll(p => p.Product == product);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(p => p.Product.Price * p.Quantity);
        }

   

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }
}
