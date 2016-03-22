using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiArchitecture.Models;

namespace WebApiArchitecture.Controllers
{
    public class ProductController : ApiController
    {
        public List<Product> GetProduct()
        {
            List<Product> lstProducts = new List<Product>();
            lstProducts.Add(new Product() { Id = 1, Name = "Product A" });
            lstProducts.Add(new Product() { Id = 2, Name = "Product B" });
            lstProducts.Add(new Product() { Id = 3, Name = "Product C" });
            lstProducts.Add(new Product() { Id = 4, Name = "Product D" });

            return lstProducts;
        }
    }
}
