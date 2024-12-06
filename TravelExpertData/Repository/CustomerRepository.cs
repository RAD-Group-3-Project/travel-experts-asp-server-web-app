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

    }
}
