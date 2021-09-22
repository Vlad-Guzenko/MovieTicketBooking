using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MovieTicketBooking.Scenarious
{
    public class AddMovieScenario:IRunnable
    {
        private List<Movie> _movies { get; set; }
        //private List<Comment> _comments { get; set; }
        private string _pathToMovies { get; set; }

        public AddMovieScenario(List<Movie> movies, string pathToMovies)
        {
            _movies = movies;
            _pathToMovies = pathToMovies;
        }

        public void Run()
        {
            Console.WriteLine("------Adding new movie------");

            Console.WriteLine("Type movie title: ");
            var movieTitle = Console.ReadLine();
            Console.WriteLine("Enter seats quantity: ");
            int seatsQuantity = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter movie genre: ");
            string movieGenre = Console.ReadLine();

            Console.WriteLine("Enter the movie rating: ");
            float movieRating = float.Parse(Console.ReadLine());

            _movies.Add(new Movie(Guid.NewGuid(), movieTitle, seatsQuantity, movieGenre, new List<Comment>() , (float)Math.Round(movieRating, 1)));

            Console.WriteLine("Added!");

            File.WriteAllText(_pathToMovies, JsonConvert.SerializeObject(_movies, Formatting.Indented));

            Console.WriteLine("Press Backspace to go back...");
        }
    }
}
