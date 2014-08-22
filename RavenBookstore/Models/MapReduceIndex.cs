using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RavenBookstore.Models
{
    public class MapReduceIndex : AbstractIndexCreationTask<Book, MapReduceIndex.ReduceResult>
    {
        public class ReduceResult
        {
            public int Year { get; set; }
            public int Count { get; set; }
        }

        public MapReduceIndex()
        {

            Map = docs => from doc in docs
                          select new
                          {
                              doc.Title,
                              doc.Author,
                              doc.Price,
                              Year = doc.YearPublished
                          };

            Reduce = results => from r in results
                                group r by r.Year
                                    into g
                                    select new
                                    {
                                        Year = g.Key,
                                        Count = g.Sum(x => x.Count)
                                    };
        }
    }
}