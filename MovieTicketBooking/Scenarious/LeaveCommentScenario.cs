using MovieTicketBooking.Repositories;
using System;

namespace MovieTicketBooking.Scenarious
{
    public class LeaveCommentScenario : IRunnable
    {
        private MovieRepository _movieRepository;

        public LeaveCommentScenario(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void Run()
        {
            Console.WriteLine("------Leaving a review------");
            Console.WriteLine("Select movie number: ");

            var movieNumber = int.Parse(Console.ReadLine());
            var selectedMovie = _movieRepository.SelectMovie(movieNumber);

            Console.WriteLine("Enter you name: ");
            string nameEntered = Console.ReadLine();

            Console.WriteLine("Type your review: ");
            string reviewTyped = Console.ReadLine();

            selectedMovie.Comments.Add( new Comment(nameEntered, reviewTyped));

            _movieRepository.Save();

            Console.WriteLine("New comment has been created!");
            Console.WriteLine("Press backspace to return");
        }
    }
}
