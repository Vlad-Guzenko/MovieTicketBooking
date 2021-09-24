using MovieTicketBooking.Repositories;
using System;

namespace MovieTicketBooking.Scenarious
{
    public class AddMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;

        public AddMovieScenario(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void Run()
        {
            Console.WriteLine("------Adding new movie------");

            Console.WriteLine("Type movie title: ");
            var movieTitle = Console.ReadLine();
            Console.WriteLine("Enter movie genre: ");
            string movieGenre = Console.ReadLine();
            Console.WriteLine("Type movie year: ");
            int movieYear = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the movie rating: ");
            float movieRating = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter seats quantity: ");
            int seatsQuantity = int.Parse(Console.ReadLine());

            Console.WriteLine("Added!");

            var newMovie = Movie.New(movieTitle, movieGenre, movieYear, movieRating, seatsQuantity);

            _movieRepository.Create(newMovie);

            _movieRepository.Save();

            Console.WriteLine("Press Backspace to go back...");
        }
    }
}
