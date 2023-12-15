using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Root.Enums
{
    public enum FilmCategory
    {
        [Display(Name = "Action & Adventure")]
        ActionAdventure = 28,
        Animation = 16,
        [Display(Name = "Mix & General")]
        MixGeneral = 1,
        Comedy = 35,
        Crime = 80,
        Documentary = 99,
        Drama = 18,
        Fantasy = 14,
        Horror = 27,
        Mystery = 9648,
        Romance = 10749,
        Thriller = 53,
        [Display(Name = "History & Politics")]
        HistoryPolitics = 36,
        Western = 37,
        Music = 10402,
        [Display(Name = "Soap & Talk")]
        SoapTalk = 10767,
        Kids = 10762,
        [Display(Name = "Science Fiction")]
        ScienceFiction = 878,
        Reality = 10764,
    }
}