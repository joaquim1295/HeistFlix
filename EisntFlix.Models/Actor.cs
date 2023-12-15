using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Models
{
    public class Actor : Person
    {
		//Relationships

		public List<Actor_Movie>? Actors_Movies { get; set; }

        public List<Actor_Serie>? Actors_Series { get; set; }

	}
}
