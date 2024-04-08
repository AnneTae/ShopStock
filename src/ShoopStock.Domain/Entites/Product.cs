# nullable disable
namespace ShoopStock.Domain.Entites;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Code { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
