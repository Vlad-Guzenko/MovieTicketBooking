using System;
namespace MovieTicketBooking.Domain.Exceptions
{
    public class NoSeatsException : Exception
    {
        public NoSeatsException(string message) : base(message){}
    }
}
