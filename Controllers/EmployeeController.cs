using Microsoft.AspNetCore.Mvc;

public class EmployeeController : Controller
{
  // this controller depends on the NorthwindRepository
  private DataContext _dataContext;
  public EmployeeController(DataContext db) => _dataContext = db;
  public IActionResult Discount() => View(_dataContext.Discounts.OrderBy(d => d.Title));
  public IActionResult Index(int id){
    ViewBag.id = id;
    return View(_dataContext.Discounts.OrderBy(d => d.Title));
  }
}
