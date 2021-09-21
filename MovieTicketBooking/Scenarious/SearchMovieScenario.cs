using ConsoleTables;
using MovieTicketBooking.Scenarious.SearchMenuScenarious;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    class SearchMovieScenario : IRunnable
    {
        private List<Movie> _movies { get; set; }
        private List<BookedMovie> _bookings;
        private string _pathToMovies;
        private string _pathToBookings;



        public SearchMovieScenario(List<Movie> movies, List<BookedMovie> bookings, string pathToMovies, string pathToBookings)
        {
            _movies = movies;
            _bookings = bookings;
            _pathToMovies = pathToMovies;
            _pathToBookings = pathToBookings;
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
                var foundMovie = _movies.Where(movie => movie.Title.ToLower()
                                                                   .Contains(stringToSearch) ||
                                                                   movie.Genre.ToLower().Contains(stringToSearch) ||
                                                                   movie.Rating.ToString(specifier)== stringToSearch.ToString()).First();
                var tab = new ConsoleTable("Title", "Free Seats", "Genre", "Rating");
                tab.AddRow(foundMovie.Title, foundMovie.FreeSeats, foundMovie.Genre, foundMovie.Rating.ToString(specifier));
                tab.Write(Format.Alternative);

                Console.WriteLine("\n1. Show movie comments\n2. Book a movie");

                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new ShowCommentsOfSpecificMovieScenario(foundMovie).ShowCommentsOfSpecificMovie();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        new BookSpecificMovieScenario(foundMovie.Id, _movies, _bookings, _pathToMovies, _pathToBookings).Run();
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
