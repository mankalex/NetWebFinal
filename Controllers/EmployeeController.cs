using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

public class EmployeeController : Controller
{
  // this controller depends on the NorthwindRepository
  private DataContext _dataContext;
  public EmployeeController(DataContext db) => _dataContext = db;
  public IActionResult Discount() => View(_dataContext.Discounts.OrderBy(d => d.Title));
  [Authorize(Roles = "employees")]
  public IActionResult Add(int id) {
    ViewBag.Products = _dataContext.Products;
    return View(_dataContext.Discounts.FirstOrDefault(di => di.DiscountId == id));
  }
  [Authorize(Roles = "employees")]
  public IActionResult Edit(int id) {
    ViewBag.Products = _dataContext.Products.OrderBy(p => p.ProductName);
    return View(_dataContext.Discounts.FirstOrDefault(di => di.DiscountId == id));
  }

// [Authorize(Roles = "employees")]
[HttpPost, ValidateAntiForgeryToken] 
public IActionResult Edit(Discount ndiscount)
{
    if (ModelState.IsValid)
    {
        try
        {
            _dataContext.EditDiscount(ndiscount);
            return RedirectToAction("Discount");
        }
        catch (DbUpdateConcurrencyException)
        {
            ModelState.AddModelError("", "Concurrency error occurred.");
            return View(ndiscount);
        }
    }
    else
    {
        
        return View(ndiscount);
    }

    
}
[Authorize(Roles = "employees")]
[HttpPost, ValidateAntiForgeryToken] 
public IActionResult Add(Discount ndiscount)
{
    if (ModelState.IsValid)
    {
        try
        {
            _dataContext.AddDiscount(ndiscount);
            return RedirectToAction("Discount");
        }
        catch (DbUpdateConcurrencyException)
        {
            ModelState.AddModelError("", "Concurrency error occurred.");
            return View(ndiscount);
        }
    }
    else
    {
        
        return View(ndiscount);
    }
}
    


[Authorize(Roles = "employees")]
public IActionResult DeleteDiscount(int id)
  {
    _dataContext.RemoveDiscount(_dataContext.Discounts.FirstOrDefault(d => d.DiscountId == id));
    return RedirectToAction("Discount");
  }    
}




