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
    public IActionResult Admin(string firstName, string lastName, string Line1, string Line2, string State, string City, int? ZipCode, string Phone)
    {
        AdminModel model = new AdminModel(_context);

        // Perform the join and filtering
        var query = _context.Customers.Join(
            _context.Addresses,
            customer => customer.AddressId, // Assuming AddressId is the foreign key in Customer
            address => address.AddressId,
            (customer, address) => new { Customer = customer, Address = address });

        // Apply filters
        if (!string.IsNullOrEmpty(firstName))
        {
            query = query.Where(x => x.Customer.FirstName.ToLower() == firstName.ToLower());
        }
        if (!string.IsNullOrEmpty(lastName))
        {
            query = query.Where(x => x.Customer.LastName.ToLower() == lastName.ToLower());
        }
        if (!string.IsNullOrEmpty(Line1))
        {
            query = query.Where(x => x.Address.Line1.ToLower() == Line1.ToLower());
        }
        if (!string.IsNullOrEmpty(Line2))
        {
            query = query.Where(x => x.Address.Line2.ToLower() == Line2.ToLower());
        }
        if (!string.IsNullOrEmpty(State))
        {
            query = query.Where(x => x.Address.State.ToLower() == State.ToLower());
        }
        if (!string.IsNullOrEmpty(City))
        {
            query = query.Where(x => x.Address.City.ToLower() == City.ToLower());
        }
        if (ZipCode.HasValue)
        {
            query = query.Where(x => x.Address.ZipCode == ZipCode.Value);
        }
        if (!string.IsNullOrEmpty(Phone))
        {
            query = query.Where(x => x.Address.Phone.ToLower() == Phone.ToLower());
        }

        // Project the results into Customer objects with the associated Address
        var results = query.Select(x => x.Customer).ToList();

        if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(Line1) && string.IsNullOrEmpty(Line2) && string.IsNullOrEmpty(State) && string.IsNullOrEmpty(City) && !ZipCode.HasValue && string.IsNullOrEmpty(Phone))
        {
            results = new List<Customer>();
        }

        model.Customers = results;

        return View("Admin", model);
    }






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
