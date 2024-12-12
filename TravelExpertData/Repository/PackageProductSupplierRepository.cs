using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository;
public class PackageProductSupplierRepository
{
    public static List<PackagesProductsSupplier> GetPackagesProductsSupplierByPackageId(TravelExpertContext context, int packageId)
    {
        return context.PackagesProductsSuppliers.Where(pps => pps.PackageId == packageId && pps.IsActive == true).ToList();
    }
}
