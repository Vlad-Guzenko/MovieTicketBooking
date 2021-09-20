using System;
namespace MovieTicketBooking.Exceptions
{
    class NoSuchTitleMovieException:Exception
    {
        public NoSuchTitleMovieException(string message):base(message){}
    }
}
