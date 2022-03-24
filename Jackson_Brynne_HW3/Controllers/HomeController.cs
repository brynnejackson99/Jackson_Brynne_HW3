using Microsoft.AspNetCore.Mvc;
using Jackson_Brynne_HW3.DAL;
using Jackson_Brynne_HW3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Jackson_Brynne_HW3.Controllers
{
    public class HomeController : Controller
    {
        //Create an instance of the db_context
        private AppDbContext _context;

        //Create the constructor so that we get an instance of AppDbContext
        //the instance of AppDbContext called _context helps us retrieve data from the database
        public HomeController(AppDbContext dbContext)
        {
            _context = dbContext;
        }


        //GET: Home
        public IActionResult Index(String SearchString)
        {
            var query = from b in _context.Books
                        select b;

            if (SearchString != null)
            {
                //if search string isn't null execute a LINQ query to limit books whole title or description contain the requested string
                query = query.Where(b => b.Title.Contains(SearchString) ||
                                    b.Description.Contains(SearchString));
            }

            //execute the query
            List<Book> SelectedBooks = query.Include(b => b.Genre).ToList();

            //populate the view bag with a ount of all books
            ViewBag.AllBooks = _context.Books.Count();
            //populate the view bag with a count of selected books
            ViewBag.SelectedBooks = SelectedBooks.Count;

            return View(SelectedBooks.OrderBy(s=>s.Title));
        }

        public IActionResult Details(int? id)//id is the id of the book you want to see
        {
            if (id == null) //BookID not specified
            {
                //user did not specify a BookID – take them to the error view
                return View("Error", new String[] { "BookID not specified - which book do you want to view?" });
            }

            //look up the book in the database based on the id; be sure to include the genre
            Book book = _context.Books.Include(b => b.Genre)
                                      .FirstOrDefault(b => b.BookID == id);

            if (book == null) //No book with this id exists in the database
            {
                //there is not a book with this id in the database – show the user an error view
                return View("Error", new String[] { "Book not found in database" });
            }

            //if code gets this far, all is well – display the details
            return View(book);
        }


        //create a detailed dearch method that returns the detailed search view
        public IActionResult DetailedSearch()
        {
            //populate the viewbag with a select list with all possible values of genres in the action method
            ViewBag.AllGenres = GetAllGenres();
            return View();
        }

        public ActionResult DisplaySearchResults(SearchViewModel svm)
        {
            var query = from b in _context.Books
                        select b;
            //*************************************************************************************
            //Code for string result
            if (svm.SearchName != null && svm.SearchName != "") //user entered something
            {
                //if search string isn't null execute a LINQ query to limit books whoe title or description contain the requested string
                query = query.Where(b => b.Title.Contains(svm.SearchName) ||
                                    b.Author.Contains(svm.SearchName));
            }
            //Code for description
            if (svm.DescriptionName != null && svm.DescriptionName != "") //user entered something
            {
                //if search string isn't null execute a LINQ query to limit books whoe title or description contain the requested string
                query = query.Where(b => b.Description.Contains(svm.DescriptionName));
            }


            //*************************************************************************************
            //Code for date
            if (svm.SelectedDate != null)//They selected a date
            {
                query = query.Where(b => b.PublishedDate >= svm.SelectedDate);
            }

            //*************************************************************************************
            //Code for radio buttons
            if (svm.SearchPrice != null)
            {
                switch (svm.Great_Less_Than)
                {
                    case Price.Greater:
                        query = query.Where(b => b.Price >= svm.SearchPrice);
                        break;
                    case Price.Less:
                        query = query.Where(b => b.Price <= svm.SearchPrice);
                        break;
                    default: //they didn't pick any of the "real" classifications
                             //They didn't select anything, so you can leave this section blank
                        break;
                } //end of select case
            }


            //*************************************************************************************
            //Code for drop-down list with enum
            if (svm.SelectedFormat == Format.Hardback || svm.SelectedFormat == Format.Paperback)
            {
                //if search string isn't null execute a LINQ query to limit books whoe title or description contain the requested string
                query = query.Where(b => b.BookFormat == svm.SelectedFormat);
            }

            //*************************************************************************************
            //Code for drop-down list with property
            //Selected genre is the selected value from the dropdown
            if (svm.SelectedGenreID != 0) //they picked a Genre
            {
                //In this example, we are just displaying the search criteria
                //In a real search, you would put a query here that selects records
                //with the same MonthID
                //Looking up the month is only necessary because we want to display the month name
                //In a search you can query records with just the ID value
                query = query.Where(b => b.Genre.GenreID == svm.SelectedGenreID);
            }

            //go to the search view
            //in a 'real' search, you would execute the query here and pass the selected records to the view
            //execute the query
            List<Book> SelectedBooks = query.Include(b => b.Genre).ToList();

            //populate the view bag with a ount of all books
            ViewBag.AllBooks = _context.Books.Count();
            //populate the view bag with a count of selected books
            ViewBag.SelectedBooks = SelectedBooks.Count;

            return View("Index", SelectedBooks.OrderBy(b => b.Title));
        }

        //create a get all genres method 
        private SelectList GetAllGenres()
        {
            //get the list of genres from the database
            List<Genre> genreList = _context.Genres.ToList();

            //add a dummy entry so the user can select all genres
            Genre SelectNone = new Genre() { GenreID = 0, GenreName = "All Genres" };
            genreList.Add(SelectNone);

            //convert the list to a SelectList by calling SelectList constructor
            //GenreID and GenreName are the names of the properties on the Genre class
            //GenreID is the primary key
            SelectList genreSelectList = new SelectList(genreList.OrderBy(g => g.GenreID), "GenreID", "GenreName");

            //return the SelectList
            return genreSelectList;
        }

    }
}
