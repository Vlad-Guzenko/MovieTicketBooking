using MovieTicketBooking.Exceptions;
using MovieTicketBooking.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Scenarious.SearchMenuScenarious
{
    public class BookSpecificMovieScenario:IRunnable
    {
        private MovieRepository _movieRepository;
        private Movie _specificMovie;
        private BookingRepository _bookingRepository;

        public BookSpecificMovieScenario(MovieRepository movieRepository, Movie specificMovie, BookingRepository bookingRepository)
        {
            _movieRepository = movieRepository;
            _specificMovie = specificMovie;
            _bookingRepository = bookingRepository;
        }

        public void Run()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"Movie selected: {_specificMovie.Title}");

                _specificMovie.ValidateAvailableSeats();

                Console.WriteLine("Type your name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Type your surname: ");
                string surname = Console.ReadLine();

                Console.WriteLine("Enter your phone number: ");
                string phoneNumber = Console.ReadLine();

                Console.WriteLine("Enter seats quantity: ");
                int requestedSeats = int.Parse(Console.ReadLine());

                _specificMovie.BookRequestedSeats(requestedSeats);

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
        }
    }
}
