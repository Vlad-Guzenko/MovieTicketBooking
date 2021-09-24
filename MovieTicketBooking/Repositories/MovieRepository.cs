using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Repositories
{
    public class MovieRepository
    {
        private List<Movie> _movies;
        private string _pathToMovies = @"../../Files\Movies.json";

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

        public Movie FindMovie(string stringToSearch, string specifier)
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

        public Movie SelectMovie(int movieNumber)
        {
            return _movies.ElementAt(movieNumber - 1);
        }

        public void RemoveMovie(Movie selectedMovie)
        {
            _movies.Remove(selectedMovie);
        }

        public void Save()
        {
            File.WriteAllText(_pathToMovies, JsonConvert.SerializeObject(_movies, Formatting.Indented));
        }

        public void SortMoviesBy()
        {
            Console.WriteLine("\nBy what criterion make sort?");
            Console.WriteLine("1. Title \n2. Available seats \n3. Genre \n4. Comments quantity \n5. Year \n6. Rating");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    _movies = _movies.OrderBy(movie => movie.Title).ToList();
                    Save();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    _movies = _movies.OrderByDescending(movie => movie.FreeSeats).ToList();
                    Save();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    _movies = _movies.OrderBy(movie => movie.Genre).ToList();
                    Save();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    _movies = _movies.OrderByDescending(movie => movie.Comments.Count).ToList();
                    Save();
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    _movies = _movies.OrderByDescending(movie => movie.Year).ToList();
                    Save();
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    _movies = _movies.OrderByDescending(movie => movie.Rating).ToList();
                    Save();
                    break;
            }
            Console.WriteLine("Press Backspace to go back...");
            
        }



        public List<Movie> GetPage()
        {
            return _movies.Skip((1) * 10).Take(10).ToList();
        }
    }
}