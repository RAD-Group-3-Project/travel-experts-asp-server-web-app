using Microsoft.EntityFrameworkCore;
using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository
{
    public class PackagesRepository
    {
        // get all packages
        public static List<Package> GetPackages(TravelExpertContext db)
        {
            return db.Packages.Where(p=> p.IsActive==true).Include(p => p.PackagesProductsSuppliers).ToList();
            //return Queryable.OrderBy<Package, string>(db.Packages.Include(p => p.PackagesProductsSuppliers), p => p.PkgName).ToList();
        }

        // add package
        public static void AddPackage(TravelExpertContext db, Package package)
        {
            db.Packages.Add(package);
            db.SaveChanges();
        }

        // update package
        public static void UpdatePackage(TravelExpertContext db, int id, Package newPackageInput)
        {
            Package? package = db.Packages.Find(id);

            // if package is found
            if (package != null)
            {
                package.PkgName = newPackageInput.PkgName;
                package.PkgBasePrice = newPackageInput.PkgBasePrice;
                package.PkgStartDate = newPackageInput.PkgStartDate;
                package.PkgEndDate = newPackageInput.PkgEndDate;
                package.PkgDesc = newPackageInput.PkgDesc;
                db.SaveChanges();
            }
        }

        // delete package
        public static void DeletePackage(TravelExpertContext db, int id)
        {
            Package package = db.Packages.Find(id);

            if (package != null)
            {
                db.Packages.Remove(package);
                db.SaveChanges();
            }
        }

        // get package by id
        public static Package? GetPackageById(TravelExpertContext db, int id)
        {
            return db.Packages.Find(id);
        }
    }
}
