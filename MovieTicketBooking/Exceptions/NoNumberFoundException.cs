using System;
namespace MovieTicketBooking.Exceptions
{
    public class NoNumberFoundException:Exception
    {
        public NoNumberFoundException(string message) : base(message) { }
    }
}
