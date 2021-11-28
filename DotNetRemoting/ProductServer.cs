using MyProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetRemoting
{
    public class ProductServer : MarshalByRefObject, IProduct
    {
        public Product Find()
        {
            return new Product
            {
                Id = "P01",
                Name = "Product 1",
                Price = 100
            };
        }

        public List<Product> FindAll()
        {
            var products = new List<Product>();
            products.Add(new Product
            {
                Id = "P01",
                Name = "Product 1",
                Price = 100
            });
            products.Add(new Product
            {
                Id = "P02",
                Name = "Product 2",
                Price = 200
            });
            products.Add(new Product
            {
                Id = "P03",
                Name = "Product 3",
                Price = 300
            });

            return products;
        }
    }
}
