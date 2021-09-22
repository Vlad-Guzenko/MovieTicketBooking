using MovieTicketBooking.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    public class BookMovieScenario : IRunnable
    {
        private List<Movie> _movies;
        private List<BookedMovie> _bookings;
        private string _pathToMovies;
        private string _pathToBookings;

        public BookMovieScenario(List<Movie> movies, List<BookedMovie> bookings, string pathToMovies, string pathToBookings)
        {
            _movies = movies;
            _bookings = bookings;
            _pathToMovies = pathToMovies;
            _pathToBookings = pathToBookings;
        }

        public void Run()
        {
            Console.WriteLine("-----Booking a movie-----");
            Console.WriteLine("Enter a movie number: ");

            try
            {
                int movieNumber = int.Parse(Console.ReadLine());
                var selectedMovie = _movies.ElementAt(movieNumber - 1);

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

                _bookings.Add(new BookedMovie(selectedMovie.Id, name, surname, phoneNumber, requestedSeats));

                Console.WriteLine("The new reservation was successfuly added!");

                File.WriteAllText(_pathToMovies, JsonConvert.SerializeObject(_movies, Formatting.Indented));
                File.WriteAllText(_pathToBookings, JsonConvert.SerializeObject(_bookings, Formatting.Indented));
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
