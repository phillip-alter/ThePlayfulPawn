using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThePlayfulPawn.Models;
using ThePlayfulPawn.Data;
using Microsoft.IdentityModel.Tokens;

namespace ThePlayfulPawn.Controllers;

public class HomeController : Controller
{
    private readonly PawnDbContext _context;

    public HomeController(PawnDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult BGSearch()
    {
        BGSearchModel model = new BGSearchModel(null, null, _context);
        return View(model);
    }

    [HttpPost]
    public IActionResult BGSearch(string? inputBGName, int? inputPlayerCount)
    {
        BGSearchModel model = new BGSearchModel(inputBGName, inputPlayerCount, _context);
        model.Search();
        return View("BGSearch", model);
    }





    [HttpGet]
    public IActionResult Admin(string firstName, string lastName, string Line1, string Line2, string State, string City, int? ZipCode, string Phone, string vendorName, string contactFirst, string contactLast) // Added vendor parameters
    {
        AdminModel model = new AdminModel(_context);

        // Customer and Address Lookup
        var customerQuery = _context.Customers.Join(
            _context.Addresses,
            customer => customer.AddressId,
            address => address.AddressId,
            (customer, address) => new { Customer = customer, Address = address });

        // Apply Customer Filters
        if (!string.IsNullOrEmpty(firstName))
        {
            customerQuery = customerQuery.Where(x => x.Customer.FirstName.ToLower() == firstName.ToLower());
        }
        if (!string.IsNullOrEmpty(lastName))
        {
            customerQuery = customerQuery.Where(x => x.Customer.LastName.ToLower() == lastName.ToLower());
        }
        if (!string.IsNullOrEmpty(Line1))
        {
            customerQuery = customerQuery.Where(x => x.Address.Line1.ToLower() == Line1.ToLower());
        }
        if (!string.IsNullOrEmpty(Line2))
        {
            customerQuery = customerQuery.Where(x => x.Address.Line2.ToLower() == Line2.ToLower());
        }
        if (!string.IsNullOrEmpty(State))
        {
            customerQuery = customerQuery.Where(x => x.Address.State.ToLower() == State.ToLower());
        }
        if (!string.IsNullOrEmpty(City))
        {
            customerQuery = customerQuery.Where(x => x.Address.City.ToLower() == City.ToLower());
        }
        if (ZipCode.HasValue)
        {
            customerQuery = customerQuery.Where(x => x.Address.ZipCode == ZipCode.Value);
        }
        if (!string.IsNullOrEmpty(Phone))
        {
            customerQuery = customerQuery.Where(x => x.Address.Phone.ToLower() == Phone.ToLower());
        }

        var customerResults = customerQuery.Select(x => x.Customer).ToList();

        if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(Line1) && string.IsNullOrEmpty(Line2) && string.IsNullOrEmpty(State) && string.IsNullOrEmpty(City) && !ZipCode.HasValue && string.IsNullOrEmpty(Phone))
        {
            customerResults = new List<Customer>();
        }

        model.Customers = customerResults;

        // Vendor and Address Lookup
        var vendorQuery = _context.Vendors.Join(
            _context.Addresses,
            vendor => vendor.VendorAddressID, // VendorAddressID is now an int
            address => address.AddressId,
            (vendor, address) => new { Vendor = vendor, Address = address });

        // Apply Vendor Filters
        if (!string.IsNullOrEmpty(vendorName))
        {
            vendorQuery = vendorQuery.Where(x => x.Vendor.VendorName.ToLower() == vendorName.ToLower());
        }
        if (!string.IsNullOrEmpty(contactFirst))
        {
            vendorQuery = vendorQuery.Where(x => x.Vendor.ContactFirst.ToLower() == contactFirst.ToLower());
        }
        if (!string.IsNullOrEmpty(contactLast))
        {
            vendorQuery = vendorQuery.Where(x => x.Vendor.ContactLast.ToLower() == contactLast.ToLower());
        }
        if (!string.IsNullOrEmpty(Line1))
        {
            vendorQuery = vendorQuery.Where(x => x.Address.Line1.ToLower() == Line1.ToLower());
        }
        if (!string.IsNullOrEmpty(Line2))
        {
            vendorQuery = vendorQuery.Where(x => x.Address.Line2.ToLower() == Line2.ToLower());
        }
        if (!string.IsNullOrEmpty(State))
        {
            vendorQuery = vendorQuery.Where(x => x.Address.State.ToLower() == State.ToLower());
        }
        if (!string.IsNullOrEmpty(City))
        {
            vendorQuery = vendorQuery.Where(x => x.Address.City.ToLower() == City.ToLower());
        }
        if (ZipCode.HasValue)
        {
            vendorQuery = vendorQuery.Where(x => x.Address.ZipCode == ZipCode.Value);
        }
        if (!string.IsNullOrEmpty(Phone))
        {
            vendorQuery = vendorQuery.Where(x => x.Address.Phone.ToLower() == Phone.ToLower());
        }

        var vendorResults = vendorQuery.Select(x => x.Vendor).ToList();

        if (string.IsNullOrEmpty(vendorName) && string.IsNullOrEmpty(contactFirst) && string.IsNullOrEmpty(contactLast) && string.IsNullOrEmpty(Line1) && string.IsNullOrEmpty(Line2) && string.IsNullOrEmpty(State) && string.IsNullOrEmpty(City) && !ZipCode.HasValue && string.IsNullOrEmpty(Phone))
        {
            vendorResults = new List<Vendor>();
        }

        model.Vendors = vendorResults;

        return View("Admin", model);
    }





    #region Customer CRUD

    [HttpGet]
    public IActionResult AddCustomer()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddCustomer(Customer customer, Address address)
    {
        _context.Addresses.Add(address);
        _context.SaveChanges();

        customer.AddressId = address.AddressId;
        _context.Customers.Add(customer);
        _context.SaveChanges();

        return RedirectToAction("Admin");
    }

    [HttpGet]
    public IActionResult EditCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
        {
            return NotFound();
        }
        var address = _context.Addresses.Find(customer.AddressId);

        if (address == null)
        {
            return NotFound();
        }

        ViewBag.Address = address;
        return View(customer);
    }

    [HttpPost]
    public IActionResult EditCustomer(Customer customer, Address address)
    {
        // Retrieve the existing address from the database
        var existingAddress = _context.Addresses.Find(customer.AddressId);

        if (existingAddress == null)
        {
            return NotFound(); // Or handle the error appropriately
        }

        // Update the properties of the existing address
        existingAddress.Line1 = address.Line1;
        existingAddress.Line2 = address.Line2;
        existingAddress.City = address.City;
        existingAddress.State = address.State;
        existingAddress.ZipCode = address.ZipCode;
        existingAddress.Phone = address.Phone;

        // Update customer properties.
        _context.Customers.Update(customer);

        // Save the changes to the database
        _context.SaveChanges();

        return RedirectToAction("Admin");
    }

    [HttpGet]
    public IActionResult DeleteCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        _context.SaveChanges();

        return RedirectToAction("Admin");
    }

    #endregion

    #region Vendor CRUD

    [HttpGet]
    public IActionResult AddVendor()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddVendor(Vendor vendor, Address address)
    {
        _context.Addresses.Add(address);
        _context.SaveChanges();

        vendor.VendorAddressID = address.AddressId;
        _context.Vendors.Add(vendor);
        _context.SaveChanges();

        return RedirectToAction("Admin");
    }

    [HttpGet]
    public IActionResult EditVendor(int id)
    {
        var vendor = _context.Vendors.Find(id);
        if (vendor == null)
        {
            return NotFound();
        }
        var address = _context.Addresses.Find(vendor.VendorAddressID);

        if (address == null)
        {
            return NotFound();
        }

        ViewBag.Address = address;
        return View(vendor);
    }

    [HttpPost]
    public IActionResult EditVendor(Vendor vendor, Address address)
    {
        // Retrieve the existing address from the database
        var existingAddress = _context.Addresses.Find(vendor.VendorAddressID);

        if (existingAddress == null)
        {
            return NotFound(); // Or handle the error appropriately
        }

        // Update the properties of the existing address
        existingAddress.Line1 = address.Line1;
        existingAddress.Line2 = address.Line2;
        existingAddress.City = address.City;
        existingAddress.State = address.State;
        existingAddress.ZipCode = address.ZipCode;
        existingAddress.Phone = address.Phone;

        // Update vendor properties.
        _context.Vendors.Update(vendor);

        // Save the changes to the database
        _context.SaveChanges();

        return RedirectToAction("Admin");
    }

    [HttpGet]
    public IActionResult DeleteVendor(int id)
    {
        var vendor = _context.Vendors.Find(id);
        if (vendor == null)
        {
            return NotFound();
        }

        _context.Vendors.Remove(vendor);
        _context.SaveChanges();

        return RedirectToAction("Admin");
    }

    #endregion






































    public IActionResult Reservations()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
