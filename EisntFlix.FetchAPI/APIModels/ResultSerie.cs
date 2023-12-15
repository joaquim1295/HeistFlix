using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.FetchAPI.APIModels
{
        public class ResultSerie
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Overview { get; set; }
            public string? Poster_Path { get; set; }
            public DateTime? First_air_date { get; set; }
            public float Vote_Average { get; set; }
            public int[]? Genre_Ids { get; set; }
        }
    
}
