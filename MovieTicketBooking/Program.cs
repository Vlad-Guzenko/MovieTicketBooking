using System;

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

            uiHelper.RenderMoviesTable();
            uiHelper.RenderMainMenu();

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Backspace:
                        Console.Clear();
                        uiHelper.RenderMoviesTable();
                        uiHelper.RenderMainMenu();
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new SearchMovieScenario(movieRepository, bookingRepository).Run();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        new SortMoviesByCriterias(movieRepository).Run();
                        uiHelper.RenderMoviesTable();
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
                }
            }
            while (keyInfo.Key != ConsoleKey.X);
        }
    }
}
