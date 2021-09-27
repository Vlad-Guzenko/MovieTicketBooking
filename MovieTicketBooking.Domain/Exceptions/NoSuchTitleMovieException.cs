using System;
namespace MovieTicketBooking.Domain.Exceptions
{
    class NoSuchTitleMovieException : Exception
    {
        public NoSuchTitleMovieException(string message):base(message){}
    }
}
