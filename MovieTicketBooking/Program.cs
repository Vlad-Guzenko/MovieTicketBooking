using System.Collections.Generic;
using System;
using ConsoleTables;

using MovieTicketBooking.Exceptions;
using MovieTicketBooking.Scenarious;
using MovieTicketBooking.Helpers;
using MovieTicketBooking.Repositories;

namespace MovieTicketBooking
{
    class Program
    {
        static void Main()
        {
            var movieRepository = new MovieRepository();
            var bookingRepository = new BookingRepository();

            var uiHelper = new UIHelper(movieRepository, bookingRepository);

            int pageNumber = 1;

            uiHelper.RenderMoviesTable(ref pageNumber);
            uiHelper.RenderMainMenu();

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Backspace:
                        Console.Clear();
                        uiHelper.RenderMoviesTable(ref pageNumber);
                        uiHelper.RenderMainMenu();
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new SearchMovieScenario(movieRepository, bookingRepository).Run();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        new SortMoviesByCriterias(movieRepository).Run();
                        uiHelper.RenderMoviesTable(ref pageNumber);
                        uiHelper.RenderMainMenu();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        new BookMovieScenario(movieRepository, bookingRepository).Run();
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        new CancelBookScenario(movieRepository, bookingRepository).Run();
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        new AddMovieScenario(movieRepository).Run();
                        break;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        new DeleteMovieScenario(movieRepository, bookingRepository).Run();
                        break;

                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        new ShowCommentsScenario(movieRepository).Run();
                        break;

                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        new LeaveCommentScenario(movieRepository).Run();
                        break;

                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        uiHelper.RenderBookings();
                        break;

                    case ConsoleKey.LeftArrow:
                        uiHelper.PrevPage(ref pageNumber);
                        uiHelper.RenderMoviesTable(ref pageNumber);
                        uiHelper.RenderMainMenu();
                        break;

                    case ConsoleKey.RightArrow:
                        uiHelper.NextPage(ref pageNumber);
                        uiHelper.RenderMoviesTable(ref pageNumber);
                        uiHelper.RenderMainMenu();
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.X);
        }
    }

    public class Movie
    {
        public Guid Id = Guid.NewGuid();
        public string Title { get; set; }
        public int FreeSeats { get; set; }
        public string Genre { get; set; }
        public List<Comment> Comments { get; set; }
        public int Year { get; set; }
        public float Rating { get; set; }

        private Movie() { }

        private Movie(Guid id, string title, int freeseats, string genre, List<Comment> comments, int year, float rating)
        {
            Id = id;
            Title = title;
            FreeSeats = freeseats;
            Genre = genre;
            Comments = comments;
            Year = year;
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

        public static Movie New(string movieTitle, string movieGenre, int year, float movieRating, int seatsQuantity)
        {
            return new Movie(Guid.NewGuid(), movieTitle, seatsQuantity, movieGenre, new List<Comment>(),year, movieRating);
        }

        internal void AddComment(string nameEntered, string reviewTyped)
        {
            Comments.Add(Comment.New(nameEntered, reviewTyped));
        }
    }

    public class BookedMovie
    {
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public int SeatsQuantity { get; set; }

        public BookedMovie() { }

        private BookedMovie(Guid id, string name, string surname, string phoneNumber, int seatsQuantity)
        {
            MovieId = id;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            SeatsQuantity = seatsQuantity;
        }

        public static BookedMovie New(Guid id, string name,string surname,string phoneNumber, int requestedSeats)
        {
            return new BookedMovie(id, name, surname, phoneNumber, requestedSeats);
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

        private Comment(){ }

        private Comment(string user, string review)
        {
            User = user;
            Text = review;
        }

        internal static Comment New(string nameEntered, string reviewTyped)
        {
            return new Comment(nameEntered, reviewTyped);
        }
    }
}
