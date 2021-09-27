using System;

namespace MovieTicketBooking.Domain.Entities
{
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
    }
}
