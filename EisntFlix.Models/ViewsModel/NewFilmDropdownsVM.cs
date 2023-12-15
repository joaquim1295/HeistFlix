using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Models.ViewsModel
{
    public class NewFilmDropdownsVM : Film
    {

        public NewFilmDropdownsVM()
        {
            Producers = new List<Producer>();
            Streamings = new List<Streaming>();
            Actors = new List<Actor>();
        }


        public List<Producer> Producers { get; set; }
        public List<Streaming> Streamings { get; set; }
        public List<Actor> Actors { get; set; }
    }


}
