using ConsoleTables;
using MovieTicketBooking.Repositories;
using System;

namespace MovieTicketBooking.Scenarious
{
    public class ShowCommentsScenario : IRunnable
    {
        private MovieRepository _movieRepository;

        public ShowCommentsScenario(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void Run()
        {
            Console.WriteLine("\nEnter a movie number: ");
            int movieNumber = int.Parse(Console.ReadLine());
            var movieSelected = _movieRepository.SelectMovie(movieNumber);

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
