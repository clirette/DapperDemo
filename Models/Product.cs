using System.ComponentModel.DataAnnotations;

namespace DapperDemo.Models
{
  public class Product
  {
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
  }
}