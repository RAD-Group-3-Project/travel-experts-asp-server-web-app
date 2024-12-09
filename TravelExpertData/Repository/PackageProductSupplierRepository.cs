using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository;
public class PackageProductSupplierRepository
{
    public static List<PackagesProductsSupplier> GetPackagesProductsSupplierByPackageId(TravelExpertContext context, int packageId)
    {
        return context.PackagesProductsSuppliers.Where(p => p.PackageId == packageId).ToList();
    }
}
