using ConsoleTables;
using System;
using System.Collections.Generic;

namespace MovieTicketBooking.Scenarious
{
    class ShowAllBookingsScenario:IRunnable
    {
        public List<BookedMovie> _bookings { get; set; }
        public ShowAllBookingsScenario(List<BookedMovie> bookings)
        {
            _bookings = bookings;
        }
        public void Run()
        {
            Console.Clear();
            var tab = new ConsoleTable("Name", "Surname", "Phone", "Seats", "Id");
            _bookings.ForEach(booking =>
            {
                tab.AddRow(booking.Name, booking.Surname, booking.PhoneNumber, booking.SeatsQuantity, booking.MovieId);
            });
            tab.Write(Format.Alternative);

            Console.WriteLine("Press backspace to return");
        }
    }
}
