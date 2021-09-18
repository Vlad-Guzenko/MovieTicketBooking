using System.Collections.Generic;
using System;
using System.Linq;
using MovieTicketBooking.Exceptions;
using System.IO;
using Newtonsoft.Json;

namespace MovieTicketBooking.Scenarious
{
    class CancelBookScenario : IRunnable
    {
        private List<Movie> _movies;
        private List<BookedMovie> _bookings;
        private string _pathToMovies;
        private string _pathToBookings;

        public CancelBookScenario(List<Movie> movies, List<BookedMovie> bookings, string pathToMovies, string pathToBookings)
        {
            _movies = movies;
            _bookings = bookings;
            _pathToMovies = pathToMovies;
            _pathToBookings = pathToBookings;
        }

        public void Run()
        {
            Console.WriteLine("------Cancel a booking------");
            Console.WriteLine("Enter a movie number: ");
            try
            {
                int movieNumber = int.Parse(Console.ReadLine());
                var selectedMovie = _movies.ElementAt(movieNumber - 1);

                Console.Clear();
                Console.WriteLine($"Movie selected: {selectedMovie.Title}");

                Console.WriteLine("Type your phone number: ");
                string phoneNumberEntered = Console.ReadLine();
                
                var bookingToCancel = _bookings.Where(booking => booking.PhoneNumber == phoneNumberEntered).Last();

                selectedMovie.ValidateReservationOnCurrentMovie(bookingToCancel.Id);

                _bookings.Remove(bookingToCancel);

                selectedMovie.ReturnSeats(bookingToCancel.SeatsQuantity);

                bookingToCancel.ShowCurrentBooking();

                File.WriteAllText(_pathToMovies, JsonConvert.SerializeObject(_movies, Formatting.Indented));
                File.WriteAllText(_pathToBookings, JsonConvert.SerializeObject(_bookings, Formatting.Indented));
            }
            catch (NoReservationsOnMovie exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
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
