using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RavenBookstore.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int YearPublished { get; set; }
        public List<string> Departments { get; set; }
        public List<CustomerReview> CustomerReviews { get; set; }
        public string CoverImageUrl { get; set; }
        public List<string> Tags { get; set; }
    }
}