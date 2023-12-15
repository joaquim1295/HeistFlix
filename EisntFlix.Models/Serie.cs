using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Models
{
    public class Serie : Film
    {
        public Serie()
        {
            Type = "Series";
        }

        public List<Actor_Serie> Actors_Series { get; set; }
    }
}
