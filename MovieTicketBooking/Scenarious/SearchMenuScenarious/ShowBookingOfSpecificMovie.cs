using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Scenarious.SearchMenuScenarious
{
    class ShowBookingOfSpecificMovie : IRunnable
    {
        public Movie _movie { get; set; }
        public List<BookedMovie> _bookings { get; set; }
        private Guid _id;
        public ShowBookingOfSpecificMovie(Movie movie, List<BookedMovie> bookings, Guid id)
        {
            _movie = movie;
            _bookings = bookings;
            _id = id;
        }
        public void Run()
        {
            Console.WriteLine();
            _bookings = _bookings.Where(booking => booking.MovieId == _id).ToList();
            var tab = new ConsoleTable("Name", "Surname", "PhoneNumber", "SeatsQuantity");
            _bookings.ForEach(booking =>
            {
                tab.AddRow(booking.Name, booking.Surname, booking.PhoneNumber, booking.SeatsQuantity);
            });
            tab.Write(Format.Alternative);
        }
    }
}
