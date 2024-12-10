using Microsoft.EntityFrameworkCore;
using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository;
public class AgencyRepository
{
    public static List<Agency> GetAgencies(TravelExpertContext dbContext)
    {
        return dbContext.Agencies.Where(a=> a.IsActive==true).Include(a => a.Agents).Where(a=> a.IsActive==true).ToList();
    }
}
