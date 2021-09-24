using MovieTicketBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTicketBooking.Repositories
{
    public class MovieRepository
    {
        private readonly FileContext _context;

        public MovieRepository()
        {
            _context = new FileContext();
        }
        public List<Movie> GetAll()
        {
            return _context.Movies;
        }

        public Movie GetById(Guid id)
        {
            return _context.Movies.Where(movie => movie.Id == id).First();
        }

        public Movie FindMovieByCriteria(string stringToSearch, string specifier)
        {
            return _context.Movies.Where(movie => movie.Title.ToLower()
                    .Contains(stringToSearch) ||
                    movie.Genre.ToLower().Contains(stringToSearch) ||
                    movie.Rating.ToString(specifier) == stringToSearch.ToString()).First();
        }

        public void Create(Movie newMovie)
        {
            _context.Movies.Add(newMovie);
        }

        public Movie GetMovie(int index)
        {
            return _context.Movies.ElementAt(index);
        }

        public void RemoveMovie(Movie selectedMovie)
        {
            _context.Movies.Remove(selectedMovie);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}