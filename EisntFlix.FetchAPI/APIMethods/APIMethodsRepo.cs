using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.FetchAPI.APIMethods
{
    public class APIMethodsRepo
    {
        public static int IndexGenreIdConverter(int id) =>
            id switch
            {
                0 => 1,
                12 or 10759 => 28,
                1759 => 16,
                10765 => 878,
                10767 => 10766,
                10768 => 36,
                _ => id
            };

    }
}
