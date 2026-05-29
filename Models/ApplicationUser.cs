using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BTVN6.Models;

public class ApplicationUser : IdentityUser
{
    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Vui lòng nhập họ tên")]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Display(Name = "Địa chỉ")]
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;
}
