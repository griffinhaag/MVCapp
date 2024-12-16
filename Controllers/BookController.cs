using System.Linq;
using System.Web.Mvc;
using BookStoreMVCApp.Models;

namespace BookStoreMVCApp.Controllers
{
    [Authorize] // Require authentication for all actions by default
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AddBook
        [Authorize(Roles = "Manager")]
        public ActionResult AddBook()
        {
            return View();
        }

        // POST: AddBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult AddBook(Book model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;
                db.Books.Add(model);
                db.SaveChanges();
                ViewBag.Message = "Book added successfully!";
            }
            return View(model);
        }

        // GET: SellBook
        [HttpGet]
        [Authorize(Roles = "SalesExecutive")]
        public ActionResult SellBook()
        {
            var books = db.Books.ToList(); // Fetch all books from the database
            return View(books); // Pass the list of books to the view
        }

        // POST: SellBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SalesExecutive")]
        public ActionResult SellBook(int bookId, int quantity)
        {
            var book = db.Books.FirstOrDefault(b => b.BookID == bookId);

            if (book == null || book.Quantity < quantity)
            {
                ViewBag.ErrorMessage = "Invalid book or insufficient quantity.";
                return RedirectToAction("SellBook");
            }

            // Reduce the book quantity
            book.Quantity -= quantity;
            db.SaveChanges();

            ViewBag.SuccessMessage = $"Successfully sold {quantity} copies of {book.Title}.";
            return RedirectToAction("SellBook");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
