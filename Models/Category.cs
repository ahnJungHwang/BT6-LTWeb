using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTVN6.Models;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
    [StringLength(50)]
    [Display(Name = "Tên danh mục")]
    public string Name { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}
