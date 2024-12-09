using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository;
public static class BookingDetailRepository
{
    public static void AddBookingDetail(TravelExpertContext context, BookingDetail bookingDetail)
    {
        context.BookingDetails.Add(bookingDetail);
        context.SaveChanges();
    }
}
