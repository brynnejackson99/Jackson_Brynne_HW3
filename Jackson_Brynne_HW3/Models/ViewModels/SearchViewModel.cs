using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Jackson_Brynne_HW3.Models
{
    //create an enum for greater or less than price
    public enum Price { Greater, Less}

    public class SearchViewModel
    {
        public Int32 SearchViewModelID { get; set; }

        [Display(Name = "Title:")]
        public String SearchName { get; set; }

        [Display(Name = "Description:")]
        public String DescriptionName { get; set; }


        [Display(Name = "Format:")]
        //This is nullable so they can select the "All formats" option that doesn't exist in the enum
        public Format? SelectedFormat { get; set; }

        [Display(Name = "Genre:")]
        public Int32 SelectedGenreID { get; set; }

        [Display(Name = "Price:")]
        public Decimal? SearchPrice { get; set; }

        [Display(Name = "Greater than or less than selected price")]
        public Price Great_Less_Than { get; set; }

        [Display(Name = "Select a released after date:")]
        [DataType(DataType.Date)]
        //DateTime?  means this date is nullable - we want to allow them to 
        //be able to NOT select a date
        public DateTime? SelectedDate { get; set; }
    }
}