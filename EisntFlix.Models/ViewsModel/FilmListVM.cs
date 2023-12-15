using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Models.ViewsModel
{
    public class FilmListVM : Film
    {
        public FilmListVM() { }

        [DisplayName("All Movies")]
        public IEnumerable<Movie>? Allmovies { get; set; }

        [DisplayName("All Series")]
        public IEnumerable<Serie>? Allseries { get; set; }

		[DisplayName("All Content")]
		public IEnumerable<Film>? Allcontent { get; set; }

		public Movie Movie { get; set; }

        public Serie Serie { get; set; }

        public Film Content { get; set; }   
        
        public dynamic DynamicContent { get; set; }
    }
}
