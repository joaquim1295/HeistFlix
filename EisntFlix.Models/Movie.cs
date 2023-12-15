using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Models
{
    public class Movie : Film
    {
        public Movie()
        {
            Type = "Movies";
        }
 
        //Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
