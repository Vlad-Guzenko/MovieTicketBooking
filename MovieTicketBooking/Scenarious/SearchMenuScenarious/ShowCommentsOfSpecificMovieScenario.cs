using ConsoleTables;
using System;

namespace MovieTicketBooking.Scenarious.SearchMenuScenarious
{
    class ShowCommentsOfSpecificMovieScenario
    {
        public Movie _movie { get; set; }
        public ShowCommentsOfSpecificMovieScenario(Movie movie)
        {
            _movie = movie;
        }
        public void ShowCommentsOfSpecificMovie()
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
