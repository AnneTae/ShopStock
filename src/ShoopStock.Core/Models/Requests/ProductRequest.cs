#nullable disable

namespace ShoopStock.Core.Models.Requests;

public class ProductRequest
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Code { get; set; }
}