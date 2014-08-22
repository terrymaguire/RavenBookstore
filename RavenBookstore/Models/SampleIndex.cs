using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RavenBookstore.Models
{
    public class SampleIndex : AbstractIndexCreationTask<Book>
    {
        public SampleIndex()
        {

            Map = docs => from doc in docs
                          select new
                          {
                              doc.Title,
                              doc.Author,
                              doc.Price,
                              Year = doc.YearPublished
                          };
        }
    }
}