using MovieTicketBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;


namespace MovieTicketBooking.Domain.Entities
{
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

        public void BookRequestedSeats(int requestedSeats)
        {
            if (FreeSeats < requestedSeats)
            {
                throw new NotEnoughtSeatsException("Our apologies, there's no free seats available you need!");
            }
            FreeSeats = FreeSeats - requestedSeats;
        }

        public void ReturnSeats(int seatsToReturn)
        {
            FreeSeats = FreeSeats + seatsToReturn;
        }

        public void ValidateAvailableSeats()
        {
            if (FreeSeats == 0)
            {
                throw new NoSeatsException($"There's no free seats for {Title}!");
            }
        }

        public static Movie New(string movieTitle, string movieGenre, int year, float movieRating, int seatsQuantity)
        {
            return new Movie(Guid.NewGuid(), movieTitle, seatsQuantity, movieGenre, new List<Comment>(), year, movieRating);
        }

        public void AddComment(string nameEntered, string reviewTyped)
        {
            Comments.Add(Comment.New(nameEntered, reviewTyped));
        }
    }

    public class Comment
    {
        public string User { get; set; }
        public string Text { get; set; }

        private Comment() { }

        private Comment(string user, string review)
        {
            User = user;
            Text = review;
        }

        public static Comment New(string nameEntered, string reviewTyped)
        {
            return new Comment(nameEntered, reviewTyped);
        }
    }
}
