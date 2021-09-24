using ConsoleTables;
using MovieTicketBooking.Repositories;
using MovieTicketBooking.Scenarious.SearchMenuScenarious;
using System;

namespace MovieTicketBooking.Scenarious
{
    public class SearchMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;
        private BookingRepository _bookingRepository;

        public SearchMovieScenario(MovieRepository movieRepository, BookingRepository bookingRepository)
        {
            _movieRepository = movieRepository;
            _bookingRepository = bookingRepository;
        }

        public void Run()
        {
            Console.Clear();
            try
            {
                ConsoleKeyInfo keyInfo;

                var specifier = "0.0";
                Console.WriteLine("Enter string to search: ");
                string stringToSearch = Console.ReadLine().ToLower();

                Movie foundMovie = _movieRepository.FindMovieByCriteria(stringToSearch, specifier);

                var tab = new ConsoleTable("Title", "Free Seats", "Genre", "Rating");
                tab.AddRow(foundMovie.Title, foundMovie.FreeSeats, foundMovie.Genre, foundMovie.Rating.ToString(specifier));
                tab.Write(Format.Alternative);

                Console.WriteLine("\n1. Show movie comments\n2. Book a movie" + "\n3. Show bookings");

                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new ShowCommentsOfSpecificMovieScenario(foundMovie).Run();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        new BookSpecificMovieScenario(_movieRepository, foundMovie, _bookingRepository).Run();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        new ShowBookingOfSpecificMovie(_bookingRepository, foundMovie.Id).Run();
                        break;
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine();
                Console.WriteLine("Movie with such title, genre or rating not found!");
            }
            Console.WriteLine("Press backspace to go back");
        }
    }
}
