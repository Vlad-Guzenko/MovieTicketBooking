using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    public class DeleteMovieScenario: IRunnable
    {
        private List<Movie> _movies { get; set; }
        private List<BookedMovie> _bookings { get; set; }
        private string _pathToMovies { get; set; }
        private string _pathToBookings { get; set; }

        public DeleteMovieScenario(List<Movie> movies, List<BookedMovie> bookings, string pathToMovies, string pathToBookings)
        {
            _movies = movies;
            _bookings = bookings;
            _pathToMovies = pathToMovies;
            _pathToBookings = pathToBookings;
        }

        public void Run()
        {
            Console.WriteLine("------Deleting Movie------");

            Console.WriteLine("Select movie number: ");

            var movieNumber = int.Parse(Console.ReadLine());
            var selectedMovie = _movies.ElementAt(movieNumber - 1);

            _movies.Remove(selectedMovie);

            _bookings.RemoveAll(bookings => bookings.MovieId == selectedMovie.Id);

            File.WriteAllText(_pathToMovies, JsonConvert.SerializeObject(_movies, Formatting.Indented));
            File.WriteAllText(_pathToBookings, JsonConvert.SerializeObject(_bookings, Formatting.Indented));

            Console.WriteLine("Deleted!");
            Console.WriteLine("Press backspace to go back...");
        }
    }
}
