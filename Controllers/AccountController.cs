using System.Web.Mvc;

namespace BookStoreMVCApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            // My authentication
            if (username == "manager@example.com" && password == "SecurePassword123!")
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("AddBook", "Book");
            }
            else if (username == "sales@example.com" && password == "SecurePassword123!")
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("SellBook", "Book");
            }

            ViewBag.ErrorMessage = "Invalid login details. Please try again.";
            return View();
        }

        // GET: Logout
        [AllowAnonymous]
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        // GET: Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Passwords do not match.";
                return View();
            }

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Email and Password are required.";
                return View();
            }

            // Dummy registration logic (to be replaced with ASP.NET Identity in the future)
            ViewBag.SuccessMessage = $"User '{email}' registered successfully.";
            return View();
        }
    }
}
