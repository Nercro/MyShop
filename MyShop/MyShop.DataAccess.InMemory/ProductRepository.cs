using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }
        }

        //stores data in cache
        public void Commit()
        {
            cache["products"] = products;
        }

        //inserts new product in list
        public void Insert(Product newProduct)
        {
            products.Add(newProduct);
        }

        //updates existitng product from list
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.ID == product.ID);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        //finds and returns project by ID
        public Product Find(string id)
        {
            Product product = products.Find(p => p.ID == id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        //returns products list as queryable
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        //deletes product from list
        public void Delete(string id)
        {
            Product productToDelete = products.Find(p => p.ID == id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
