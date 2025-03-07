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
    private readonly PawnRepo<Game> _gameRepo;
    private readonly PawnRepo<Address> _addressRepo;
    private readonly PawnRepo<Customer> _customerRepo;
    private readonly PawnRepo<Reservation> _reservationRepo;
    private readonly PawnRepo<Food> _foodRepo;
    private readonly PawnRepo<Vendor> _vendorRepo;

    public HomeController(PawnDbContext context, PawnRepo<Game> gameRepo, PawnRepo<Address> addressRepo, PawnRepo<Customer> customerRepo,
                          PawnRepo<Reservation> reservationRepo, PawnRepo<Food> foodRepo, PawnRepo<Vendor> vendorRepo)
    {
        _gameRepo = gameRepo;
        _addressRepo = addressRepo;
        _customerRepo = customerRepo;
        _reservationRepo = reservationRepo;
        _foodRepo = foodRepo;
        _vendorRepo = vendorRepo;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult BGSearch(string? search, int? playerCount)
    {
        List<Game> bgList = _gameRepo.GetAll().ToList();
        if (!search.IsNullOrEmpty()){
            bgList = bgList.Where(g => g.GameName.ToLower().Contains(search)).ToList();
        }
        if (playerCount != null){
            bgList = bgList.Where(g => g.MaxPlayerCount <= playerCount).ToList();
        }
        return View(bgList);
    }


    [HttpGet]
    public IActionResult Admin(string firstName, string lastName, string Line1, string Line2, string State, string City, int? ZipCode, string Phone, string vendorName, string contactFirst, string contactLast) // Added vendor parameters
    {
        AdminModel model = new AdminModel();
        model.Customers = _customerRepo.GetAll().ToList();
        model.Addresses = _addressRepo.GetAll().ToList();
        model.Vendors = _vendorRepo.GetAll().ToList();
        
        if (!string.IsNullOrEmpty(firstName))
        {
            model.Customers = model.Customers.Where(c => c.FirstName.ToLower().Contains(firstName.ToLower())).ToList();
        }
        if (!string.IsNullOrEmpty(lastName))
        {
            model.Customers = model.Customers.Where(c => c.LastName.ToLower().Contains(lastName.ToLower())).ToList();
        }
        if (!string.IsNullOrEmpty(Line1))
        {
            model.Addresses = model.Addresses.Where(a => a.Line1.ToLower().Contains(Line1.ToLower())).ToList();
        }
        if (!string.IsNullOrEmpty(Line2))
        {
            model.Addresses = model.Addresses.Where(a => a.Line2.ToLower().Contains(Line2.ToLower())).ToList();
        }
        if (!string.IsNullOrEmpty(State))
        {
            model.Addresses = model.Addresses.Where(a => a.State.ToLower().Contains(State.ToLower())).ToList();
        }
        if (!string.IsNullOrEmpty(City))
        {
            model.Addresses = model.Addresses.Where(a => a.City.ToLower().Contains(City.ToLower())).ToList();
        }
        if (ZipCode.HasValue)
        {
            model.Addresses = model.Addresses.Where(a => a.ZipCode == ZipCode).ToList();
        }
        if (!string.IsNullOrEmpty(Phone))
        {
            model.Addresses = model.Addresses.Where(a => a.Phone.ToLower().Contains(Line1.ToLower())).ToList();
        }
        if (!string.IsNullOrEmpty(vendorName))
        {
            model.Vendors = model.Vendors.Where(c => c.VendorName.ToLower().Contains(vendorName.ToLower())).ToList();
        }
        if (!string.IsNullOrEmpty(contactFirst))
        {
            model.Vendors = model.Vendors.Where(c => c.ContactFirst.ToLower().Contains(contactFirst.ToLower())).ToList();
        }
        if (!string.IsNullOrEmpty(contactLast))
        {
            model.Vendors = model.Vendors.Where(c => c.ContactLast.ToLower().Contains(contactLast.ToLower())).ToList();
        }

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
        _addressRepo.Add(address);
        customer.AddressId = address.AddressId;
        _customerRepo.Add(customer);

        return RedirectToAction("Admin");
    }

    [HttpGet]
    public IActionResult EditCustomer(int id)
    {
        var customer = _customerRepo.GetById(id);
        if (customer == null)
        {
            return NotFound();
        }
        var address = _addressRepo.GetById(customer.AddressId);

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
        var existingAddress = _addressRepo.GetById(customer.AddressId);

        if (existingAddress == null)
        {
            return NotFound();
        }

        existingAddress.Line1 = address.Line1;
        existingAddress.Line2 = address.Line2;
        existingAddress.City = address.City;
        existingAddress.State = address.State;
        existingAddress.ZipCode = address.ZipCode;
        existingAddress.Phone = address.Phone;

        _customerRepo.Update(customer);

        return RedirectToAction("Admin","Home");
    }

    [HttpGet]
    public IActionResult DeleteCustomer(int id)
    {
        var customer = _customerRepo.GetById(id);
        if (customer == null)
        {
            return NotFound();
        }
        List<Reservation> reservations = _reservationRepo.GetAll().ToList(); 
        foreach (var reservation in reservations)
        {
            _reservationRepo.Delete(reservation.ReservationId); 
        }
        _customerRepo.Delete(id);

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
        _addressRepo.Add(address);
        vendor.VendorAddressID = address.AddressId;
        _vendorRepo.Add(vendor);
        return RedirectToAction("Admin");
    }

    [HttpGet]
    public IActionResult EditVendor(int id)
    {
        var vendor = _vendorRepo.GetById(id);
        if (vendor == null)
        {
            return NotFound();
        }
        var address = _addressRepo.GetById(id);

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
        var existingAddress = _addressRepo.GetById(vendor.VendorAddressID);

        if (existingAddress == null)
        {
            return NotFound(); 
        }

        existingAddress.Line1 = address.Line1;
        existingAddress.Line2 = address.Line2;
        existingAddress.City = address.City;
        existingAddress.State = address.State;
        existingAddress.ZipCode = address.ZipCode;
        existingAddress.Phone = address.Phone;

        _vendorRepo.Update(vendor);

        return RedirectToAction("Admin");
    }

    [HttpGet]
    public IActionResult DeleteVendor(int id)
    {
        var vendor = _vendorRepo.GetById(id);
        if (vendor == null)
        {
            return NotFound();
        }
        _vendorRepo.Delete(id);
        return RedirectToAction("Admin");
    }

    #endregion

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
