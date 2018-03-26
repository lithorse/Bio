using Bio.Models;
using System;
using System.Linq;

namespace Bio.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CinemaContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Auditoriums.Any())
            {
                return;   // DB has been seeded
            }

            var auditoriums = new Auditorium[]
            {
            new Auditorium{Seats=50},
            new Auditorium{Seats=100},
            };
            foreach (Auditorium a in auditoriums)
            {
                context.Auditoriums.Add(a);
            }
            context.SaveChanges();

            var movies = new Movie[]
            {
            new Movie{Name="Tough guys don't dance",Length=109},
            new Movie{Name="My son, my son what have ye done",Length=91},
            new Movie{Name="Masked and anonymous",Length=112}
            };
            foreach (Movie m in movies)
            {
                context.Movies.Add(m);
            }
            context.SaveChanges();

            var viewings = new Viewing[]
            {
            new Viewing{Time="12:00", SeatsLeft=50, MovieID=1, AuditoriumID=1},
            new Viewing{Time="12:00", SeatsLeft=100, MovieID=2, AuditoriumID=2},
            new Viewing{Time="16:00", SeatsLeft=50, MovieID=3, AuditoriumID=1},
            new Viewing{Time="16:00", SeatsLeft=100, MovieID=2, AuditoriumID=2},
            };
            foreach (Viewing v in viewings)
            {
                context.Viewings.Add(v);
            }
            context.SaveChanges();
        }
    }
}