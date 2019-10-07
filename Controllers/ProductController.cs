using System.Collections.Generic;
using DapperDemo.Models;
using DapperDemo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DapperDemo.Controllers
{
  [Route("api/[controller]")]
  public class ProductController : ControllerBase
  {
    private readonly ProductRepository productRepository;
    public ProductController()
    {
      productRepository = new ProductRepository();

    }

    [HttpGet]
    public IEnumerable<Product> Get()
    {
      return productRepository.GetAll();
    }

    [HttpGet("{id}")]
    public Product Get(int id)
    {
      return productRepository.GetById(id);
    }

    [HttpPost]
    public Product Post([FromBody]Product prod)
    {
      if (ModelState.IsValid)
      {
        return productRepository.Add(prod);
      }
      return null;
    }

    [HttpPut("{id}")]
    public Product Put(int id, [FromBody]Product prod)
    {
      prod.ProductId = id;
      if (ModelState.IsValid)
      {
        return productRepository.Update(prod);
      }
      return null;
    }

    [HttpDelete("{id}")]
    public Product Delete(int id)
    {
      return productRepository.Delete(id);
    }
  }
}