using System;
namespace MovieTicketBooking.Exceptions
{
    public class NotEnoughtSeatsException : Exception
    {
        public NotEnoughtSeatsException(string message) : base(message)
        {

        }
    }
}
