using System;

namespace MovieTicketBooking.Exceptions
{
    public class NoReservationsOnMovie:Exception
    {
        public NoReservationsOnMovie(string message) : base(message) { }
    }
}
