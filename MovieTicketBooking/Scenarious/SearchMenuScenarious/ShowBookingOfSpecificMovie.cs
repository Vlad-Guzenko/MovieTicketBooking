using ConsoleTables;
using MovieTicketBooking.Repositories;
using System;

namespace MovieTicketBooking.Scenarious.SearchMenuScenarious
{
    class ShowBookingOfSpecificMovie : IRunnable
    {
        private readonly BookingRepository _bookingRepository;
        private Guid _movieId;

        public ShowBookingOfSpecificMovie(BookingRepository bookingRepository, Guid movieId)
        {
            _bookingRepository = bookingRepository;
            _movieId = movieId;
        }

        public void Run()
        {
            Console.WriteLine();
            var bookings = _bookingRepository.GetById(_movieId);

            var tab = new ConsoleTable("Name", "Surname", "Phone Number", "Seats Quantity");
            bookings.ForEach(booking =>
            {
                tab.AddRow(booking.Name, booking.Surname, booking.PhoneNumber, booking.SeatsQuantity);
            });
            tab.Write(Format.Alternative);
        }
    }
}
