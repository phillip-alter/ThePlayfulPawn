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

        // Only filter and load customers if search parameters are provided
        if (!string.IsNullOrEmpty(firstName) ||
             !string.IsNullOrEmpty(lastName) ||
                !string.IsNullOrEmpty(Line1) ||
                !string.IsNullOrEmpty(Line2) ||
                !string.IsNullOrEmpty(State) ||
                !string.IsNullOrEmpty(City)  ||
                !string.IsNullOrEmpty(Phone))
        {
            if (!string.IsNullOrEmpty(firstName))
            {
                model.Customers = model.Customers
                    .Where(x => x.FirstName.ToLower() == firstName.ToLower())
                    .ToList();
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                model.Customers = model.Customers
                    .Where(x => x.LastName.ToLower() == lastName.ToLower())
                    .ToList();
            }
            if (!string.IsNullOrEmpty(Line1))
            {
                model.Addresses = model.Addresses
                    .Where(x => x.Line1.ToLower() == Line1.ToLower())
                    .ToList();
            }
            if (!string.IsNullOrEmpty(Line2))
            {
                model.Addresses = model.Addresses
                    .Where(x => x.Line2.ToLower() == Line2.ToLower())
                    .ToList();
            }
            if (!string.IsNullOrEmpty(State))
            {
                model.Addresses = model.Addresses
                    .Where(x => x.State.ToLower() == State.ToLower())
                    .ToList();
            }
            if (!string.IsNullOrEmpty(City))
            {
                model.Addresses = model.Addresses
                    .Where(x => x.City.ToLower() == City.ToLower())
                    .ToList();
            }
            if (ZipCode.HasValue) // Check if ZipCode has a value
            {
                model.Addresses = model.Addresses
                    .Where(x => x.ZipCode == ZipCode.Value) // Filter by the integer value
                    .ToList();
            }
            if (!string.IsNullOrEmpty(Phone))
            {
                model.Addresses = model.Addresses
                    .Where(x => x.Phone.ToLower() == Phone.ToLower())
                    .ToList();
            }

        }
        else
        {
            //ensure the model.Customers list is empty upon initial page load.
            model.Customers = new List<Customer>();
            model.Addresses = new List<Address>();
        }

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
