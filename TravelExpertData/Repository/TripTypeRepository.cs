using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository
{
    public class TripTypeRepository
    {
        public static List<TripType> getAllTrips(TravelExpertContext db)
        {
            return db.TripTypes.ToList();
        }
    }
}
