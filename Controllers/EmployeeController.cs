using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class EmployeeController : Controller
{
  // this controller depends on the NorthwindRepository
  private DataContext _dataContext;
  public EmployeeController(DataContext db) => _dataContext = db;
  public IActionResult Discount() => View(_dataContext.Discounts.OrderBy(d => d.Title));
  public IActionResult Edit(int id) {
    ViewBag.Products = _dataContext.Products.OrderBy(p => p.ProductName);
    return View(_dataContext.Discounts.FirstOrDefault(di => di.DiscountId == id));
  }

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

}
