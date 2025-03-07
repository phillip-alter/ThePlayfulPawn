using ThePlayfulPawn.Data;
using System.Collections.Generic;
using System.Linq;

namespace ThePlayfulPawn.Models
{
    public class ReservationsModel
    {
        public IEnumerable<Reservation> Reservations {get;set;} = new List<Reservation>();
        public IEnumerable<Customer> Customers {get;set;} = new List<Customer>();
    }
}