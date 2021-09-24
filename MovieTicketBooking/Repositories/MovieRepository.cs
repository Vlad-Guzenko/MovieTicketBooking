using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Repositories
{
    public class MovieRepository
    {
        private readonly List<Movie> _movies;
        private readonly string _pathToMovies = @"../../Files\Movies.json";

        public MovieRepository()
        {
            _movies = JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText(_pathToMovies));
        }
        public List<Movie> GetAll()
        {
            return _movies;
        }

        public Movie GetById(Guid id)
        {
            return _movies.Where(movie => movie.Id == id).First();
        }

        public Movie FindMovieByCriteria(string stringToSearch, string specifier)
        {
            return _movies.Where(movie => movie.Title.ToLower()
                    .Contains(stringToSearch) ||
                    movie.Genre.ToLower().Contains(stringToSearch) ||
                    movie.Rating.ToString(specifier) == stringToSearch.ToString()).First();
        }

        public void Create(Movie newMovie)
        {
            _movies.Add(newMovie);
        }

        public Movie GetMovie(int index)
        {
            return _movies.ElementAt(index);
        }

        public void RemoveMovie(Movie selectedMovie)
        {
            _movies.Remove(selectedMovie);
        }

        public void Save()
        {
            File.WriteAllText(_pathToMovies, JsonConvert.SerializeObject(_movies, Formatting.Indented));
        }

        
    }
}