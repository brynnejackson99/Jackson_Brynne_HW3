using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

//TODO: Update these using statements to match the name of your project
using Jackson_Brynne_HW3.DAL;
using Jackson_Brynne_HW3.Seeding;

//TODO: Update this namespace to match your project
namespace Jackson_Brynne_HW3.Controllers
{
    public class SeedController : Controller
    {
        //You will need an instance of the AppDbContext class for this code to work
        //Create a private variable to hold the AppDbContext object
        private AppDbContext _context;

        //Create a constructor for this class that accepts an instance of AppDbContext
        //The code in Startup.cs configures the project to provide an instance of AppDbContext
        //when Controller classes are instantiated.
        public SeedController(AppDbContext context) //context variable is  populated in this constructor
            //know this is a contructor because it is the same name as the class
        {
            //Set the private variable equal to the instance that was
            //passed into the constructor
            _context = context;
        }

        //This is the action method for the Seeding page with the two buttons
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult SeedAllGenres()
        {
            try
            {
                //call the SeedAllGenres method from your Seeding class folder
                //you will need to pass in the instance of AppDbContext
                //that you set in the constructor
                SeedGenres.SeedAllGenres(_context);
            }
            catch (Exception ex)
            {
                //add the error messages to a list of strings
                List<String> errorList = new List<String>();

                errorList.Add("There was a problem adding this genre.");

                //Add the outer message
                errorList.Add(ex.Message);

                //Add the message from the inner exception, if there is one
                if (ex.InnerException != null)
                {
                    errorList.Add(ex.InnerException.Message);
                }
                
                //Add additional inner exception messages, if there are any
                if (ex.InnerException.InnerException != null)
                {
                    errorList.Add(ex.InnerException.InnerException.Message);
                }

                //return the user to the error view
                return View("Error", errorList);
            }

            //everything is okay - send user to confirmation page
            return View("Confirm");
        }

        public IActionResult SeedAllBooks()
        {
            //this code may throw an exception, so we need to be in a Try/Catch block
            try
            {
                //call the SeedBooks method from your Seeding folder
                //you will need to pass in the instance of AppDbContext
                //that you set in the constructor
                SeedBooks.SeedAllBooks(_context);
            }
            catch (Exception ex)
            { 
                 //add the error messages to a list of strings
                    List<String> errorList = new List<String>();

                errorList.Add("There was a problem adding this book.");

                //Add the outer message
                errorList.Add(ex.Message);

                //Add the message from the inner exception, if there is one
                if (ex.InnerException != null)
                {
                    errorList.Add(ex.InnerException.Message);
                }

                //Add additional inner exception messages, if there are any
                if (ex.InnerException.InnerException != null)
                {
                    errorList.Add(ex.InnerException.InnerException.Message);
                }

                //return the user to the error view
                return View("Error", errorList);
            }

            //everything is okay - send user to confirmation page
            return View("Confirm");
        }
    }
}