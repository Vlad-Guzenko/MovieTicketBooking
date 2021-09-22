using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    public class ShowCommentsScenario : IRunnable
    {
        private List<Movie> _movies { get; set; }

        public ShowCommentsScenario(List<Movie> movies)
        {
            _movies = movies;
        }

        public void Run()
        {
            Console.WriteLine("\nEnter a movie number: ");
            int movieNumber = int.Parse(Console.ReadLine());
            var movieSelected = _movies.ElementAt(movieNumber - 1);

            Console.WriteLine();
            var tab = new ConsoleTable("Username", "Comment");
            movieSelected.Comments.ForEach(comment =>
            {
                tab.AddRow(comment.User, comment.Text);
            });
            tab.Write(Format.Alternative);
            
            Console.WriteLine("\nPress backspace to return");
        }
    }
}
