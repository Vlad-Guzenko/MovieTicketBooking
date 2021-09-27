using System;
using ConsoleTables;

using MovieTicketBooking.Infrastructure.Repositories;

namespace MovieTicketBooking.Application1.Scenarious
{
    public class ShowAllBookingsScenario : IRunnable
    {
        private BookingRepository _bookingRepository;
        private MovieRepository _movieRepository;

        public ShowAllBookingsScenario(BookingRepository bookingRepository, MovieRepository movieRepository)
        {
            _bookingRepository = bookingRepository;
            _movieRepository = movieRepository;
        }

        public void Run()
        {
            Console.Clear();

            var tab = new ConsoleTable("Name", "Surname", "Phone", "Seats", "Movie Title");

            var bookings = _bookingRepository.GetAll();

            bookings.ForEach(booking =>
            {
                tab.AddRow(booking.Name, booking.Surname, booking.PhoneNumber, _movieRepository.GetById(booking.MovieId).Title);
            });
            tab.Write(Format.Alternative);

            Console.WriteLine("Press backspace to return");
        }
    }
}
