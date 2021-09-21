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
                        new SearchMovieScenario(movies, bookings, pathToMovies, pathToBookedMovies).Run();
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
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        new AddMovieScenario(movies,pathToMovies).Run();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        new DeleteMovieScenario(movies, bookings, pathToMovies, pathToBookedMovies).Run();
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        new ShowCommentsScenario(movies).Run();
                        break;
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        new LeaveCommentScenario(movies, pathToMovies).Run();
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.X);
        }
        
        private static void RenderMainMenu()
        {
            Console.WriteLine("\n1. Search movie" + "\n2. Sort a movies" + "\n3. Book a movie" + "\n4. Booking cancelation" + "\n5. Add movie" + "\n6. Delete movie" + "\n7. Show movie comments" + "\n8. Leave a comment ");
            Console.WriteLine("\nSelect: ");
        }
        private static void RenderMoviesTable(List<Movie> movies)
        {
            var maxTitleLength = movies.Max(title => title.Title.Length);

            var titleCol = "Title";

            var rightPaddingTitle = new string(' ', maxTitleLength - titleCol.Length);

            Console.WriteLine($"| #  | {titleCol}{rightPaddingTitle} | Free Seats | Comments |");

            foreach (var movieIterator in movies.Select((item, index) => (item, index)))
            {
                var leftPad = new string(' ', maxTitleLength - movieIterator.item.Title.Length);
                
                var number = movieIterator.index + 1;
                Console.WriteLine($"| {number.ToString("D2")} | {movieIterator.item.Title}{leftPad} |     {movieIterator.item.FreeSeats.ToString("D3")}    |    {movieIterator.item.Comments.Count.ToString("D3")}   |");
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
                tab.AddRow(booking.Name, booking.Surname, booking.PhoneNumber, booking.SeatsQuantity, booking.MovieId);
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
        public string Genre { get; set; }
        public List<Comment> Comments { get; set; }
        public float Rating { get; set; }

        public Movie(Guid id, string title, int freeseats, string genre, List<Comment> comments, float rating)
        {
            Id = id;
            Title = title;
            FreeSeats = freeseats;
            Genre = genre;
            Comments = comments;
            Rating = rating;
        }

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
    }

    public class BookedMovie
    {
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public int SeatsQuantity { get; set; }

        public BookedMovie(Guid id, string name, string surname, string phoneNumber, int seatsQuantity)
        {
            MovieId = id;
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
    }

    public class Comment
    {
        public string User { get; set; }
        public string Text{ get; set; }

        public Comment(string user, string review)
        {
            User = user;
            Text = review;
        }
    }
}
