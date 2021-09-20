using System;
using System.Collections.Generic;

namespace MovieTicketBooking.Scenarious
{
    class SearchMovieScenario : IRunnable
    {
        public List<Movie> _movies { get; set; }
        public SearchMovieScenario(List<Movie> movies)
        {
            _movies = movies;
        }

        public void Run()
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter a movie title: ");
                try
                {
                    string titleToSearch = Console.ReadLine();
                    var foundMovie = _movies.Find(movie => movie.Title == titleToSearch);
                    Console.WriteLine($"|Title:  {foundMovie.Title} | Free Seats: {foundMovie.FreeSeats} |");
                }
                catch (Exception)
                {
                    Console.WriteLine();
                    Console.WriteLine("Movie with such name not found!");
                }

                Console.WriteLine("Press backspace to go back");

                keyInfo = Console.ReadKey();
            } while (keyInfo.Key != ConsoleKey.Backspace);
        }
    }
}
