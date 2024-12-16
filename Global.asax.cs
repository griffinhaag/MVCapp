using BookStoreMVCApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BookStoreMVCApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            CreateRolesAndUsers(); // Seed default roles and users
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // Method to seed roles and users
        private void CreateRolesAndUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // Create Manager role and default Manager user
                if (!roleManager.RoleExists("Manager"))
                {
                    roleManager.Create(new IdentityRole("Manager"));

                    var managerUser = new ApplicationUser
                    {
                        UserName = "manager@example.com",
                        Email = "manager@example.com"
                    };
                    var result = userManager.Create(managerUser, "SecurePassword123!");
                    if (result.Succeeded)
                    {
                        userManager.AddToRole(managerUser.Id, "Manager");
                    }
                }

                // Create SalesExecutive role and default SalesExecutive user
                if (!roleManager.RoleExists("SalesExecutive"))
                {
                    roleManager.Create(new IdentityRole("SalesExecutive"));

                    var salesUser = new ApplicationUser
                    {
                        UserName = "sales@example.com",
                        Email = "sales@example.com"
                    };
                    var result = userManager.Create(salesUser, "SecurePassword123!");
                    if (result.Succeeded)
                    {
                        userManager.AddToRole(salesUser.Id, "SalesExecutive");
                    }
                }
            }
        }
    }
}
