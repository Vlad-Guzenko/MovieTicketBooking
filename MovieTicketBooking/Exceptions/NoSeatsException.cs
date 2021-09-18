using System;
namespace MovieTicketBooking.Exceptions
{
    public class NoSeatsException : Exception
    {
        public NoSeatsException(string message) : base(message){}
    }
}
