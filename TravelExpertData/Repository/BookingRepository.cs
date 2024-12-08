using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository
{
    public class BookingRepository
    {   

        public static void AddBooking(TravelExpertContext db, Booking newBooking)
        {
            db.Add(newBooking);
            db.SaveChanges();
        }

        public static List<Booking> getBookingsById(TravelExpertContext db, int userId)
        {

            return db.Bookings
                .Where(b => b.CustomerId == userId).Include(b => b.Package).ToList();
        }
    }
}
