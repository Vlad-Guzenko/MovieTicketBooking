using MovieTicketBooking.Exceptions;
using MovieTicketBooking.Repositories;
using System;

namespace MovieTicketBooking.Scenarious
{
    public class BookMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;
        private BookingRepository _bookingRepository;

        public BookMovieScenario(MovieRepository movieRepository,  BookingRepository bookingRepository)
        {
            _movieRepository = movieRepository;
            _bookingRepository = bookingRepository;
        }

        public void Run()
        {
            Console.WriteLine("-----Booking a movie-----");
            Console.WriteLine("Enter a movie number: ");

            try
            {
                int movieNumber = int.Parse(Console.ReadLine());
                var selectedMovie = _movieRepository.GetMovie(movieNumber-1);

                selectedMovie.ValidateAvailableSeats();

                Console.Clear();
                Console.WriteLine($"Movie selected: {selectedMovie.Title}");

                Console.WriteLine("Type your name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Type your surname: ");
                string surname = Console.ReadLine();

                Console.WriteLine("Enter your phone number: ");
                string phoneNumber = Console.ReadLine();

                Console.WriteLine("Enter seats quantity: ");
                int requestedSeats = int.Parse(Console.ReadLine());

                selectedMovie.BookRequestedSeats(requestedSeats);

                var newBooking = BookedMovie.New(selectedMovie.Id, name, surname, phoneNumber, requestedSeats);

                _bookingRepository.Create(newBooking);

                _movieRepository.Save();
                _bookingRepository.Save();

                Console.WriteLine("The new reservation was successfuly added!");
            }
            catch (NotEnoughtSeatsException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            catch (NoSeatsException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            Console.WriteLine("Press Backspace to go back...");
        }
    }
}
