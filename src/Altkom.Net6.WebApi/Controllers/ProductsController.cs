using Altkom.Net6.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Altkom.Net6.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_productRepository.Get());
        }

        // GET api/customers/{id}/products 

        // GET api/customers/{id}/products
        // api/products?customerId={customerId}
        // api/customers/{customerId}/products/liked
        // api/customers/{customerId}/orders/2022/05

        // api/posts?id=123454354
        // api/posts?id=334434344

        // api/posts/netcore6/minimalapi
        // api/posts/netcore6/mvc

        [HttpGet("/api/customers/{customerId}/products")]
        public ActionResult<IEnumerable<Product>> GetByCustomer(int customerId)
        {
            return Ok(_productRepository.GetByCustomer(customerId));
        }
    }
}
