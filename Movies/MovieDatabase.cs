﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }

        public List<Movie> SearchAndFilter(string searchString, List<string> rating)
        {
            //case 0: no search string, no ratings
            if (searchString == null && rating.Count == 0) return All;

            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                // case 1: search string AND ratings
                if(searchString != null && rating.Count > 0) {
                    if (movie.Title != null && movie.Title.Contains(searchString, StringComparison.InvariantCulture) && rating.Contains(movie.MPAA_Rating))
                    {
                        results.Add(movie);
                    }
                }
                // case 2: search string only
                else if(searchString != null)
                {
                    if (movie.Title != null && movie.Title.Contains(searchString, StringComparison.InvariantCulture))
                    {
                        results.Add(movie);
                    }
                }
                
                // case 3: ratings only
                else if(rating.Count > 0)
                {
                    if (rating.Contains(movie.MPAA_Rating))
                    {
                        results.Add(movie);
                    }
                }
            }
            return results;
        }
    }
}
