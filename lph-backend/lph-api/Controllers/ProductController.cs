using lph_api.Repository.ProductRepo;
using Microsoft.AspNetCore.Mvc;

namespace lph_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        try
        {
            var products = _productRepository.GetAll();

            if (!products.Any())
            {
                return NotFound("No products in database");
            }

            return Ok(products);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error getting product data");
            return BadRequest("Error getting product data");
        }
    }

    [HttpGet("GetByName:{name}")]
    public IActionResult GetByUsername(string name)
    {
        try
        {
            var product = _productRepository.GetByName(name);
            
            if (product == null)
            {
                return NotFound("No patient with this username in database");
            }

            return Ok(product);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error getting patient data");
            return BadRequest("Error getting patient data");
        }
    }
}