using System;

using ConsoleTables;
using MovieTicketBooking.Domain.Entities;

namespace MovieTicketBooking.Application1.Scenarious.SearchMenuScenarious
{
    public class ShowCommentsOfSpecificMovieScenario : IRunnable
    {
        private readonly Movie _movie;

        public ShowCommentsOfSpecificMovieScenario(Movie movie)
        {
            _movie = movie;
        }
        public void Run()
        {
            Console.WriteLine();

            var tab = new ConsoleTable("Title", "Comments");

            _movie.Comments.ForEach(comment =>
            {
                tab.AddRow(comment.User, comment.Text);
            });

            tab.Write(Format.Alternative);
        }
    }
}
