using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        //stores data in cache
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        //inserts new product in list
        public void Insert(ProductCategory newProductCategory)
        {
            productCategories.Add(newProductCategory);
        }

        //updates existitng product from list
        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.ID == productCategory.ID);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        //finds and returns project by ID
        public ProductCategory Find(string id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.ID == id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        //returns products list as queryable
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        //deletes product from list
        public void Delete(string id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.ID == id);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
    }
}
