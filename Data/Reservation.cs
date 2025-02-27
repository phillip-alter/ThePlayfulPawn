using Microsoft.EntityFrameworkCore;

namespace ThePlayfulPawn.Data 
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int GameId { get; set; }
        public int GroupTotal { get; set; }
        public DateTime DateTime { get; set; }
    }
}