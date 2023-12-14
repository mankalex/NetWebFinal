using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options) { }

  public DbSet<Product> Products { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Discount> Discounts { get; set; }
  public DbSet<Customer> Customers { get; set; }
  public DbSet<CartItem> CartItems { get; set; }
  public DbSet<Employee> Employees { get; set; }


  public void AddCustomer(Customer customer)
  {
    Customers.Add(customer);
    SaveChanges();
  }
  public void EditCustomer(Customer customer)
  {
    var customerToUpdate = Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
    customerToUpdate.Address = customer.Address;
    customerToUpdate.City = customer.City;
    customerToUpdate.Region = customer.Region;
    customerToUpdate.PostalCode = customer.PostalCode;
    customerToUpdate.Country = customer.Country;
    customerToUpdate.Phone = customer.Phone;
    customerToUpdate.Fax = customer.Fax;
    SaveChanges();
  }
  public void EditDiscount(Discount discount)
  {
    var discountToUpdate = Discounts.FirstOrDefault(d => d.DiscountId == discount.DiscountId);
    discountToUpdate.Code = discount.Code;
    discountToUpdate.StartTime = discount.StartTime;
    discountToUpdate.EndTime = discount.EndTime;
    discountToUpdate.ProductId = discount.ProductId;
    discountToUpdate.DiscountPercent = discount.DiscountPercent;
    discountToUpdate.Title = discount.Title;
    discountToUpdate.Description = discount.Description;
    SaveChanges();
  }
  public void AddDiscount(Discount discount)
  {
    this.Add(discount);
    this.SaveChanges();
  }

  public void DeleteDiscount(Discount discount)
  {
    this.Remove(discount);
    this.SaveChanges();
  }
  public CartItem AddToCart(CartItemJSON cartItemJSON)
  {
    CartItem cartItem = new CartItem()
    {
      CustomerId = Customers.FirstOrDefault(c => c.Email == cartItemJSON.email).CustomerId,
      ProductId = cartItemJSON.id,
      Quantity = cartItemJSON.qty
    };
    CartItems.Add(cartItem);
    SaveChanges();
    cartItem.Product = Products.Find(cartItem.ProductId);
    return cartItem;
  }
}
