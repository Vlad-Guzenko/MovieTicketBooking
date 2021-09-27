using System;
namespace MovieTicketBooking.Domain.Exceptions
{
    public class NotEnoughtSeatsException : Exception
    {
        public NotEnoughtSeatsException(string message) : base(message){}
    }
}
