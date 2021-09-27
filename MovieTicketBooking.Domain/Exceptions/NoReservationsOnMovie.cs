using System;

namespace MovieTicketBooking.Domain.Exceptions
{
    public class NoReservationsOnMovie:Exception
    {
        public NoReservationsOnMovie(string message) : base(message) { }
    }
}
