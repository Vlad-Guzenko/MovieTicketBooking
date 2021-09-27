using System;
using System.Collections.Generic;
using System.Linq;

using MovieTicketBooking.Domain.Entities;

namespace MovieTicketBooking.Infrastructure.Repositories
{
    public class BookingRepository
    {
        private readonly FileContext _context;
        
        public BookingRepository()
        {
            _context = new FileContext();
        }

        public List<BookedMovie> GetById(Guid id)
        {
            return _context.Bookings.Where(booking => booking.MovieId == id).ToList();
        }

        public void Create(BookedMovie newBooking)
        {
            _context.Bookings.Add(newBooking);
        }

        public List<BookedMovie> GetAll()
        {
            return _context.Bookings;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public BookedMovie FindByPhoneNumber(string phoneNumberEntered, Movie selectedMovie)
        {
            return _context.Bookings.Where(booking => booking.PhoneNumber == phoneNumberEntered && booking.MovieId == selectedMovie.Id).First();
        }

        public void RemoveAllBookings(Movie selectedMovie)
        {
            _context.Bookings.RemoveAll(bookings => bookings.MovieId == selectedMovie.Id);
        }

        public void RemoveBooking(BookedMovie bookedMovie)
        {
            _context.Bookings.Remove(bookedMovie);
        }
    }
}
