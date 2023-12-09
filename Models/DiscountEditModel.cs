using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


public class DiscountEditModel
{
  public IdentityRole Discount { get; set; }
}

public class RoleModificationModel
{
    [Required]
  public string RoleId { get; set; }

}