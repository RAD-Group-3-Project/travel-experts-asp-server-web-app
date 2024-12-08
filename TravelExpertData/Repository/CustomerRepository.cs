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
    public class CustomerRepository
    {
       public static void AddCustomer(TravelExpertContext db , Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }
        public static int GetLastId(TravelExpertContext db)
        {
            var lastCustomer = db.Customers
                .OrderByDescending(c => c.CustomerId) // Order by CustomerId in descending order
                .FirstOrDefault(); // Get the first customer (which will be the one with the highest ID)

            int lastCustomerId = lastCustomer?.CustomerId ?? 0; // If lastCustomer is null, return 0

            return lastCustomerId;
        }

        public static Customer GetCustomerById(TravelExpertContext db, int customerId)
        {
            
            return db.Customers.SingleOrDefault(c => c.CustomerId == customerId);
        }



    }
}
