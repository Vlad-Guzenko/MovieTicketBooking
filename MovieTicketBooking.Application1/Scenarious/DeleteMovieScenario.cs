using System;

using MovieTicketBooking.Infrastructure.Repositories;

namespace MovieTicketBooking.Application1.Scenarious
{
    public class DeleteMovieScenario: IRunnable
    {
        private MovieRepository _movieRepository;
        private BookingRepository _bookingRepository;

        public DeleteMovieScenario(MovieRepository movieRepository, BookingRepository bookingRepository)
        {
            _movieRepository = movieRepository;
            _bookingRepository = bookingRepository;
        }

        public void Run()
        {
            Console.WriteLine("------Deleting Movie------");

            Console.WriteLine("Select movie number: ");

            var movieNumber = int.Parse(Console.ReadLine());
            var selectedMovie = _movieRepository.GetMovie(movieNumber-1);

            _movieRepository.RemoveMovie(selectedMovie);
            _bookingRepository.RemoveAllBookings(selectedMovie);

            _movieRepository.Save();
            _bookingRepository.Save();

            Console.WriteLine("Deleted!");
            Console.WriteLine("Press backspace to go back...");
        }
    }
}
