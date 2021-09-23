using System;
using MovieTicketBooking.Repositories;

namespace MovieTicketBooking.Scenarious
{
    class CancelBookScenario : IRunnable
    {
        private MovieRepository _movieRepository;
        private BookingRepository _bookingRepository;

        public CancelBookScenario(MovieRepository movieRepository,  BookingRepository bookingRepository)
        {
            _movieRepository = movieRepository;
            _bookingRepository = bookingRepository;
        }

        public void Run()
        {
            Console.WriteLine("------Cancel a booking------");
            Console.WriteLine("Enter a movie number: ");
            try
            {
                int movieNumber = int.Parse(Console.ReadLine());
                var selectedMovie = _movieRepository.SelectMovie(movieNumber);

                Console.Clear();
                Console.WriteLine($"Movie selected: {selectedMovie.Title}");

                Console.WriteLine("Type your phone number: ");
                string phoneNumberEntered = Console.ReadLine();

                var bookingToCancel = _bookingRepository.FindByPhoneNumber(phoneNumberEntered, selectedMovie);

                _bookingRepository.RemoveBooking(bookingToCancel);

                selectedMovie.ReturnSeats(bookingToCancel.SeatsQuantity);
                    
                bookingToCancel.ShowCurrentBooking();

                _movieRepository.Save();
                _bookingRepository.Save();
            }
            catch(InvalidOperationException)
            {
                Console.WriteLine();
                Console.WriteLine("Sorry, we have not reservations on this movie with such phone number!");
            }
            Console.WriteLine("Press Backspace to go back...");
        }
    }
}
