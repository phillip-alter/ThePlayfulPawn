using ThePlayfulPawn.Data;
using System.Collections.Generic;
using System.Linq;

namespace ThePlayfulPawn.Models
{
    public class ReservationsModel
    {
        public IEnumerable<ReservationsInputModel> Reservations = new List<ReservationsInputModel>();

    }
}