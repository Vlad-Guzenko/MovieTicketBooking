using ConsoleTables;
using MovieTicketBooking.Repositories;
using System;
using System.Linq;

namespace MovieTicketBooking.Helpers
{
    public class UIHelper
    {
        private MovieRepository _movieRepository;
        private BookingRepository _bookingRepository;

        public UIHelper(MovieRepository movieRepository, BookingRepository bookingRepository)
        {
            _movieRepository = movieRepository;
            _bookingRepository = bookingRepository;
        }

        public void RenderMoviesTable()
        {
            var movies = _movieRepository.GetAll();
            Console.Clear();
            var maxTitleLength = movies.Max(title => title.Title.Length);

            var titleCol = "Title";

            var rightPaddingTitle = new string(' ', maxTitleLength - titleCol.Length);

            Console.WriteLine($"| #  | {titleCol}{rightPaddingTitle} | Free Seats | Comments | Rating |");
            var specifier = "0.0";
            foreach (var movieIterator in movies.Select((item, index) => (item, index)))
            {
                var leftPad = new string(' ', maxTitleLength - movieIterator.item.Title.Length);

                var number = movieIterator.index + 1;
                Console.WriteLine($"| {number.ToString("D2")} | {movieIterator.item.Title}{leftPad} |     {movieIterator.item.FreeSeats.ToString("D3")}    |    {movieIterator.item.Comments.Count.ToString("D3")}   |   {movieIterator.item.Rating.ToString(specifier)}  |");
            }
        }

        public void RenderMainMenu()
        {
            Console.WriteLine("\n<- PREVIOUS " + "| NEXT ->" + "\n\n1. Search movie" + "\n2. Sort movies" + "\n3. Book a movie" + "\n4. Cancel booking" + "\n5. Add movie" + "\n6. Delete movie" + "\n7. Show movie comments" + "\n8. Leave a comment " + "\n9. Show all bookings");
            Console.WriteLine("\nSelect: ");
        }

        public void RenderBookings()
        {
            Console.Clear();
            var bookings = _bookingRepository.GetAll();
            var tab = new ConsoleTable("Name", "Surname", "Phone", "Seats", "Id");

            bookings.ForEach(booking =>
            {
                tab.AddRow(booking.Name, booking.Surname, booking.PhoneNumber, booking.SeatsQuantity, booking.MovieId);
            });
            tab.Write(Format.Alternative);
            Console.WriteLine("Press backspace to return");
        }
    }
}
