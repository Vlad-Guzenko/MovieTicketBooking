using ConsoleTables;
using MovieTicketBooking.Repositories;
using System;

namespace MovieTicketBooking.Scenarious.SearchMenuScenarious
{
    class ShowBookingOfSpecificMovie : IRunnable
    {
        public Movie _movie { get; set; }
        private BookingRepository _bookingRepository;
        private Guid _id;
        public ShowBookingOfSpecificMovie(BookingRepository bookingRepository, Guid id)
        {
            _bookingRepository = bookingRepository;
            _id = id;
        }
        public void Run()
        {
            Console.WriteLine();
            var bookings = _bookingRepository.GetById(_id);

            var tab = new ConsoleTable("Name", "Surname", "PhoneNumber", "SeatsQuantity");
            bookings.ForEach(booking =>
            {
                tab.AddRow(booking.Name, booking.Surname, booking.PhoneNumber, booking.SeatsQuantity);
            });
            tab.Write(Format.Alternative);
        }
    }
}
