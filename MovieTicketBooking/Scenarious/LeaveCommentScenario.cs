using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    public class LeaveCommentScenario : IRunnable
    {
        private List<Movie> _movies { get; set; }

        private string _pathToMovies { get; set; }

        public LeaveCommentScenario(List<Movie> movies, string pathToMovies)
        {
            _movies = movies;
            _pathToMovies = pathToMovies;
        }

        public void Run()
        {
            Console.WriteLine("------Leaving a review------");
            Console.WriteLine("Select movie number: ");

            var movieNumber = int.Parse(Console.ReadLine());
            var selectedMovie = _movies.ElementAt(movieNumber - 1);

            Console.WriteLine("Enter you name: ");
            string nameEntered = Console.ReadLine();

            Console.WriteLine("Type your review: ");
            string reviewTyped = Console.ReadLine();

            selectedMovie.Comments.Add( new Comment(nameEntered, reviewTyped));

            File.WriteAllText(_pathToMovies, JsonConvert.SerializeObject(_movies, Formatting.Indented));

            Console.WriteLine("New comment has been created!");
            Console.WriteLine("Press backspace to return");
        }
    }
}
