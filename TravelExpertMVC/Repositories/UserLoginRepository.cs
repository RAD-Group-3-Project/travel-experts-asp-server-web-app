using TravelExpertMVC.Models;
using TravelExpertMVC.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;


namespace TravelExpertMVC.Repositories
{
    public class UserLoginRepository
    {
        public static bool UserLogin(string username, string password)
        {
            using TravelExpertContext db = new TravelExpertContext();
            {
                var user = db.Users.FirstOrDefault(u => u.UserLogin == username && u.UserPassword == password);
                if (user != null) { return true; }
                else { return false; }
            }
        }
    }
}
