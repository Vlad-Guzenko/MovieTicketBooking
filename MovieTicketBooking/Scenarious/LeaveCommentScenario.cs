using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    class LeaveCommentScenario:IRunnable
    {
        public List<Movie> _movies { get; set; }

        public string _pathToMovies { get; set; }

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

            //_movies.Add(new Movie(selectedMovie.Id, selectedMovie.Title, selectedMovie.FreeSeats, selectedMovie.Genre, new List<Comment>() { new Comment(nameEntered, reviewTyped)}, selectedMovie.Rating));

            selectedMovie.Comments.Add( new Comment(nameEntered, reviewTyped));

            Console.WriteLine("New comment has been created!");

            File.WriteAllText(_pathToMovies, JsonConvert.SerializeObject(_movies, Formatting.Indented));

            Console.WriteLine("Press backspace to return");
        }
    }
}
