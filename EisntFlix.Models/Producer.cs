using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Models
{
    public class Producer : Person
    {
        public List<Movie>? Movies { get; set; }
        public List<Serie>? Series { get; set; }
    }
}