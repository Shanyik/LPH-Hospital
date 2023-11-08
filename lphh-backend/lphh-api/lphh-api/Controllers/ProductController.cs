using lphh_api.Model;
using lphh_api.Repository.ProductRepo;
using Microsoft.AspNetCore.Mvc;

namespace lphh_api.Controllers;

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
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var products = await _productRepository.GetAll();

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
    public async Task<IActionResult> GetByUsername(string name)
    {
        try
        {
            var product = await _productRepository.GetByName(name);
            
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
    public async Task<IActionResult> GetByUsername(int id)
    {
        try
        {
            var product = await _productRepository.GetById(id);
            
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