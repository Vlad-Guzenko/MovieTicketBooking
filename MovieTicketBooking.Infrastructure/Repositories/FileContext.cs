using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

using MovieTicketBooking.Domain.Entities;

namespace MovieTicketBooking.Infrastructure.Repositories
{
    public class FileContext
    {
        private readonly string _pathToBookings = @"../../../../MovieTicketBooking.Infrastructure\Files\BookedMovies.json";
        private readonly string _pathToMovies = @"../../../../MovieTicketBooking.Infrastructure\Files\Movies.json";

        private readonly List<BookedMovie> _bookings;
        private readonly List<Movie> _movies;

        public List<BookedMovie> Bookings
        {
            get
            {
                return _bookings;
            }
        }

        public List<Movie> Movies
        {
            get
            {
                return _movies;
            }
        }

        public FileContext()
        {
            _bookings = JsonConvert.DeserializeObject<List<BookedMovie>>(File.ReadAllText(_pathToBookings));
            _movies = JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText(_pathToMovies));
        }

        public void SaveChanges()
        {
            File.WriteAllText(_pathToBookings, JsonConvert.SerializeObject(Bookings, Formatting.Indented));
            File.WriteAllText(_pathToMovies, JsonConvert.SerializeObject(Movies, Formatting.Indented));
        }
    }
}
