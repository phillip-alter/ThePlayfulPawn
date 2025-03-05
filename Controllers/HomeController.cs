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
    public IActionResult Admin(string firstName, string lastName)
    {
        AdminModel model = new AdminModel(_context);

        // Only filter and load customers if search parameters are provided
        if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
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
        }
        else
        {
            //ensure the model.Customers list is empty upon initial page load.
            model.Customers = new List<Customer>();
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
