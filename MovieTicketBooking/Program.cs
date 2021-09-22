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
            int pageNumber = 1;

            ///render a table
            RenderMoviesTable(movies/*, pageNumber*/);
            RenderMainMenu();

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Backspace:
                        Console.Clear();
                        RenderMoviesTable(movies/*, pageNumber*/);
                        RenderMainMenu();
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new SearchMovieScenario(movies, bookings, pathToMovies, pathToBookedMovies).Run();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        SortMovies(movies, pathToMovies, pageNumber);
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
                        new AddMovieScenario(movies, pathToMovies).Run();
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
                    /*case ConsoleKey.LeftArrow:
                        RenderMainMenu();
                        pageNumber = pageNumber == 1 ? pageNumber = 1 : pageNumber -= 1;
                        RenderMoviesTable(movies, pageNumber);
                        RenderMainMenu();
                        break;
                    case ConsoleKey.RightArrow:
                        RenderMainMenu();
                        int max = movies.Count() % 10;
                        pageNumber = pageNumber == max ? pageNumber = max : pageNumber += 1;
                        RenderMoviesTable(movies, pageNumber);
                        RenderMainMenu();
                        break;*/
                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        new ShowAllBookingsScenario(bookings).Run();
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.X);
        }

        private static void RenderMainMenu()
        {
            Console.WriteLine("\n<- PREVIOUS " + "| NEXT ->" + "\n\n1. Search movie" + "\n2. Sort movies" + "\n3. Book a movie" + "\n4. Cancel a movie" + "\n5. Add movie" + "\n6. Delete movie" + "\n7. Show movie comments" + "\n8. Leave a comment " + "\n9. Show all bookings");
            Console.WriteLine("\nSelect: ");
        }
        private static void RenderMoviesTable(List<Movie> movies/*, int pageNumber*/)
        {
            Console.Clear();
            //movies = GetPage(movies, pageNumber);
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
        private static void SortMovies(List<Movie> movies, string path, int pageNumber)
        {
            Console.Clear();
            movies = movies.OrderBy(movie => movie.Title).ToList();
            File.WriteAllText(path, JsonConvert.SerializeObject(movies, Formatting.Indented));
            RenderMoviesTable(movies/*, pageNumber*/);
            RenderMainMenu();
        }
        /*private static List<Movie> GetPage(List<Movie> moviesArg, int pageNumber)
        {            
            return moviesArg.Skip((pageNumber - 1) * 10).Take(10).ToList();
        }*/
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
        public string Text { get; set; }

        public Comment(string user, string review)
        {
            User = user;
            Text = review;
        }
    }
}
