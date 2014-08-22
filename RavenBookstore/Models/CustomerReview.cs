using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RavenBookstore.Models
{
    public class CustomerReview
    {
        public int Rating { get; set; } // number of stars out of 5
        public string Review { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }

}