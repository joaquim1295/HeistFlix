using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.FetchAPI.APIModels
{
    public class ResultMovie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public string? Poster_Path { get; set; }
        public DateTime? Release_date { get; set; }
        public float Vote_Average { get; set; }
        public int[]? Genre_Ids { get; set; }
    }
}
