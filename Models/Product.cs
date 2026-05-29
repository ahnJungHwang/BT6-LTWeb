using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTVN6.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
    [StringLength(100)]
    [Display(Name = "Tên sản phẩm")]
    public string Name { get; set; } = string.Empty;

    [Range(1000, 100000000, ErrorMessage = "Giá phải từ 1.000 đến 100.000.000")]
    [Display(Name = "Giá bán")]
    public decimal Price { get; set; }

    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Display(Name = "Ảnh đại diện")]
    public string? ImageUrl { get; set; }

    public List<ProductImage> Images { get; set; } = new();

    [ForeignKey("Category")]
    [Display(Name = "Mã danh mục")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
