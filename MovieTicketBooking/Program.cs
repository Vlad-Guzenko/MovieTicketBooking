using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using ConsoleTables;
using MovieTicketBooking.Exceptions;
using MovieTicketBooking.Scenarious;

namespace MovieTicketBooking
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathToMovies = @"../../Files\Movies.json";
            var pathToBookedMovies = @"../../Files\BookedMovies.json";

            var moviesAsString = File.ReadAllText(pathToMovies);
            var movies = JsonConvert.DeserializeObject<List<Movie>>(moviesAsString);

            //File.WriteAllText(pathToMovies, JsonConvert.SerializeObject(movies, Formatting.Indented));

            var bookingsAsString = File.ReadAllText(pathToBookedMovies);
            var bookings = JsonConvert.DeserializeObject<List<BookedMovie>>(bookingsAsString);


            ///render a table
            RenderMoviesTable(movies);
            RenderMainMenu();

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Backspace:
                        Console.Clear();
                        RenderMoviesTable(movies);
                        RenderMainMenu();
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        SortMovies(movies, pathToMovies);
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        new BookMovieScenario(movies, bookings, pathToMovies, pathToBookedMovies).Run();
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        new CancelBookScenario(movies, bookings, pathToMovies, pathToBookedMovies).Run();
                        //ShowAllBookings(bookings);

                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.X);
        }
        
        private static void RenderMainMenu()
        {
            Console.WriteLine("\n1. Search movie" + "\n2. Sort a movies" + "\n3. Book a movie" + "\n4. Booking cancelation" + "\n5. Add movie" + "\n6. Exit");
            Console.WriteLine("\nSelect: ");
        }
        private static void RenderMoviesTable(List<Movie> movies)
        {
            var maxTitleLength = movies.Max(title => title.Title.Length);

            var titleCol = "Title";

            var rightPaddingTitle = new string(' ', maxTitleLength - titleCol.Length);

            Console.WriteLine($"| #  | {titleCol}{rightPaddingTitle} | Free Seats |");

            foreach (var movieIterator in movies.Select((item, index) => (item, index)))
            {
                var leftPad = new string(' ', maxTitleLength - movieIterator.item.Title.Length);
                
                var number = movieIterator.index + 1;
                Console.WriteLine($"| {number.ToString("D2")} | {movieIterator.item.Title}{leftPad} | {movieIterator.item.FreeSeats}          |");
            }
        }
        private static void SortMovies(List<Movie> movies, string path)
        {
            Console.Clear();
            movies = movies.OrderBy(movie => movie.Title).ToList();
            File.WriteAllText(path, JsonConvert.SerializeObject(movies, Formatting.Indented));
            RenderMoviesTable(movies);
            RenderMainMenu();
        }
        private static void ShowAllBookings(List<BookedMovie> bookings)
        {
            Console.Clear();
            var tab = new ConsoleTable("Name", "Surname", "Phone", "Seats", "Id");
            bookings.ForEach(booking =>
            {
                tab.AddRow(booking.Name, booking.Surname, booking.PhoneNumber, booking.SeatsQuantity, booking.Id);
            });
            tab.Write();
            Console.ReadLine();
        }
    }

    public class Movie
    {
        public Guid Id = Guid.NewGuid();
        public string Title { get; set; }
        public int FreeSeats { get; set; }

        internal void BookRequestedSeats(int requestedSeats)
        {
            if (FreeSeats < requestedSeats)
            {
                throw new NotEnoughtSeatsException("Our apologies, there's no free seats available you need!");
            }
            FreeSeats = FreeSeats - requestedSeats;
        }

        internal void ReturnSeats(int seatsToReturn)
        {
            FreeSeats = FreeSeats + seatsToReturn;
        }

        internal void ValidateAvailableSeats()
        {
            if (FreeSeats == 0)
            {
                throw new NoSeatsException($"There's no free seats for {Title}!");
            }
        }

        internal void ValidateReservationOnCurrentMovie(Guid id)
        {
            if (Id != id)
            {
                throw new NoReservationsOnMovie($"There's no reservations on movie {Title}!");
            }
        }

        /*internal void ValidateNoSeatsReserved(Guid id)
        {
            if ()
            {
                throw new NoSeatsException($"There's no free seats for {Title}!");
            }
        }*/
    }
    public class BookedMovie
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public int SeatsQuantity { get; set; }

        public BookedMovie(Guid id, string name, string surname, string phoneNumber, int seatsQuantity)
        {
            Id = id;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            SeatsQuantity = seatsQuantity;
        }

        public void ShowCurrentBooking()
        {
            var tab = new ConsoleTable("Name", "Surname", "Phone", "Seats");
            tab.AddRow(Name, Surname, PhoneNumber, SeatsQuantity);
            tab.Write();
        }

        /*public void ValidatePhoneNumber(string phoneNumber)
        {
            if (PhoneNumber != phoneNumber)
            {
                throw new NoNumberFoundException("Sorry, we haven't found reservations on this phone number!");
            }
        }*/
    }
}
