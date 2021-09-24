using MovieTicketBooking.Repositories;
using System;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    class SortMoviesByCriterias : IRunnable
    {
        private MovieRepository _movieRepository;

        public SortMoviesByCriterias(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void Run()
        {
            Console.WriteLine("\nBy what criterion make sort?");
            Console.WriteLine("1. Title \n2. Available seats \n3. Genre \n4. Comments quantity \n5. Year \n6. Rating");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    _movieRepository.GetAll().OrderBy(movie => movie.Title).ToList();
                    _movieRepository.Save();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    _movieRepository.GetAll().OrderByDescending(movie => movie.FreeSeats).ToList();
                    _movieRepository.Save();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    _movieRepository.GetAll().OrderByDescending(movie => movie.Genre).ToList();
                    _movieRepository.Save();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    _movieRepository.GetAll().OrderByDescending(movie => movie.Comments.Count).ToList();
                    _movieRepository.Save();
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    _movieRepository.GetAll().OrderByDescending(movie => movie.Year).ToList();
                    _movieRepository.Save();
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    _movieRepository.GetAll().OrderByDescending(movie => movie.Rating).ToList();
                    _movieRepository.Save();
                    break;
            }
            Console.WriteLine("Press Backspace to go back...");
        }
    }
}
