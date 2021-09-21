using MovieTicketBooking.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Scenarious.SearchMenuScenarious
{
    class BookSpecificMovieScenario:IRunnable
    {
        public List<Movie> _movies { get; set; }
        private List<BookedMovie> _bookings;
        private string _pathToMovies;
        private string _pathToBookings;
        private Guid _id;

        public BookSpecificMovieScenario(Guid id, List<Movie> movies, List<BookedMovie> bookings, string pathToMovies, string pathToBooking)
        {
            _movies = movies;
            _bookings = bookings;
            _pathToMovies = pathToMovies;
            _pathToBookings = pathToBooking;
            _id = id;
        }

        public void Run()
        {
            try
            {
                var foundMovie = _movies.Where(movie => movie.Id == _id).First();

                Console.Clear();
                Console.WriteLine($"Movie selected: {foundMovie.Title}");

                foundMovie.ValidateAvailableSeats();

                //enter data
                Console.WriteLine("Type your name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Type your surname: ");
                string surname = Console.ReadLine();

                Console.WriteLine("Enter your phone number: ");
                string phoneNumber = Console.ReadLine();

                Console.WriteLine("Enter seats quantity: ");
                int requestedSeats = int.Parse(Console.ReadLine());

                foundMovie.BookRequestedSeats(requestedSeats);

                _bookings.Add(new BookedMovie(foundMovie.Id, name, surname, phoneNumber, requestedSeats));

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

        }
    }
}
