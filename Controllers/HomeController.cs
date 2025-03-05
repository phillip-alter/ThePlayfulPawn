using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThePlayfulPawn.Models;
using ThePlayfulPawn.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Routing.Constraints;

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

    public IActionResult Admin()
    {
        return View();
    }
    /*------------------------------------------------------------------------------------------------------*/
    // This action method handles the GET request for the Reservations page.
    // It supports searching for reservations by the customer's first and last name.
    [HttpGet]
    public IActionResult Reservations(string? searchFName, string? searchLName, DateTime? searchDate)
    {
        // It creates a model to hold the reservations
        var model = new ReservationsModel();

        // Gets all reservations with customer details
        var query = from r in _context.Reservations
                    join c in _context.Customers on r.CustomerId equals c.CustomerId
                    select new ReservationsInputModel
                    {
                        CustomerFName = c.FirstName,
                        CustomerLName = c.LastName,
                        GroupTotal = r.GroupTotal,
                        DateTime = r.DateTime
                    };

        // Filter by first and last name if provided
        if (!string.IsNullOrEmpty(searchFName))
        {
            query = query.Where(res => res.CustomerFName.Contains(searchFName));
        }

        if (!string.IsNullOrEmpty(searchLName))
        {
            query = query.Where(res => res.CustomerLName.Contains(searchLName));
        }

        // If a date is search that is provided thne it will filter by reservation date
        if(searchDate.HasValue){
            query = query.Where(res => res.DateTime.Date == searchDate.Value.Date);
        }

        model.Reservations = query.ToList();
        return View(model);
    }
    // This POST method handles adding a new reservation. 
    // It accepts the form inputs: First Name, Last Name, Reservation Date/Time, and Group Size.
    [HttpPost]
    public IActionResult Reservations(string fname, string lname, DateTime datetime, int grouptotal)
    {
        var customer = _context.Customers.FirstOrDefault(c => c.FirstName == fname && c.LastName == lname);

        if (customer == null)
        {
            // If the customer doesn't exist, display an error message
            ModelState.AddModelError(string.Empty, "Customer does not exist!");
            return View("Reservations", new ReservationsModel());
        }

        // Create a new reservation and add it to the database
        var reservation = new Reservation
        {
            CustomerId = customer.CustomerId,
            GroupTotal = grouptotal,
            DateTime = datetime
        };

        _context.Reservations.Add(reservation);
        _context.SaveChanges();

        // Redirect to the GET action to show the updated list
        return RedirectToAction("Reservations");
    }
/*-------------------------------------------------------------------------------------------------- */
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
