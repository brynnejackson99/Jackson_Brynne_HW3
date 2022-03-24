using System;
using System.ComponentModel.DataAnnotations;

//TODO: Update this namespace to match your project
namespace Jackson_Brynne_HW3.Models
{
    public enum Format { Hardback, Paperback }
    
    public class Book
    {
        //Primary key
        public Int32 BookID { get; set; }

        //Navigational properties
        public String Title { get; set; }
        public String Author { get; set; }
        public String Description { get; set; }

        [Display(Name = "Published Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        public DateTime PublishedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Price { get; set; }

        [Display(Name = "Book Format")]
        public Format BookFormat { get; set; }

        public Genre Genre { get; set; }
    }
}
