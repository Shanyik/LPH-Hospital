using lph_api.Model;
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
                return NotFound();
            }

            return Ok(products);
        }
        catch (Exception e)
        {
            return BadRequest();
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
                return NotFound();
            }

            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("GetById:{id}")]
    public IActionResult GetByUsername(int id)
    {
        try
        {
            var product = _productRepository.GetById(id);
            
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}