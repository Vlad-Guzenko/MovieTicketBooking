using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Repositories
{
    public class BookingRepository
    {
        private List<BookedMovie> _bookings;
        private string _pathToBookings = @"../../Files\BookedMovies.json";

        public BookingRepository()
        {
            _bookings = JsonConvert.DeserializeObject<List<BookedMovie>>(File.ReadAllText(_pathToBookings));
        }

        public List<BookedMovie> GetById(Guid id)
        {
            return _bookings.Where(booking => booking.MovieId == id).ToList();
        }

        public void Create(BookedMovie newBooking)
        {
            _bookings.Add(newBooking);
        }

        public List<BookedMovie> GetAll()
        {
            return _bookings;
        }

        public void Save()
        {
            File.WriteAllText(_pathToBookings, JsonConvert.SerializeObject(_bookings, Formatting.Indented));
        }

        public BookedMovie FindByPhoneNumber(string phoneNumberEntered, Movie selectedMovie)
        {
            return _bookings.Where(booking => booking.PhoneNumber == phoneNumberEntered && booking.MovieId == selectedMovie.Id)
                                               .First();
        }

        public void RemoveAllBookings(Movie selectedMovie)
        {
            _bookings.RemoveAll(bookings => bookings.MovieId == selectedMovie.Id);
        }

        public void RemoveBooking(BookedMovie bookedMovie)
        {
            _bookings.Remove(bookedMovie);
        }
    }
}
