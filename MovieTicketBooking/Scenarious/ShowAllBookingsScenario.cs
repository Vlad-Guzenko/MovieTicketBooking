using ConsoleTables;
using MovieTicketBooking.Repositories;
using System;
using System.Collections.Generic;

namespace MovieTicketBooking.Scenarious
{
    public class ShowAllBookingsScenario : IRunnable
    {
        private BookingRepository _bookingRepository;

        public ShowAllBookingsScenario(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public void Run()
        {
            Console.Clear();

            var tab = new ConsoleTable("Name", "Surname", "Phone", "Seats", "Id");

            var bookings = _bookingRepository.GetAll();

            bookings.ForEach(booking =>
            {
                tab.AddRow(booking.Name, booking.Surname, booking.PhoneNumber, booking.SeatsQuantity, booking.MovieId);
            });
            tab.Write(Format.Alternative);

            Console.WriteLine("Press backspace to return");
        }
    }
}
