using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string basketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Product> pContext, IRepository<Basket> bContext)
        {
            basketContext = bContext;
            productContext = pContext;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNUll)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(basketSessionName);

            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketID = cookie.Value;
                if (!string.IsNullOrEmpty(basketID))
                {
                    basket = basketContext.Find(basketID);
                }
                else
                {
                    if (createIfNUll)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNUll)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }

            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(basketSessionName);
            cookie.Value = basket.ID;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.basketItems.FirstOrDefault(i => i.productID == productId);

            if (item == null)
            {
                item = new BasketItem() { basketID = basket.ID, productID = productId, quantity = 1 };

                basket.basketItems.Add(item);
            }
            else
            {
                item.quantity = item.quantity + 1;
            }

            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.basketItems.FirstOrDefault(i => i.ID == itemId);

            if (item != null)
            {
                basket.basketItems.Remove(item);
                basketContext.Commit();
            }
        }
    }
}
