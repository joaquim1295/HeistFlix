using EisntFlix.Models;
using EisntFlix.Models.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Business.Services.IServices
{
    public interface IFilmsService
    {
        Task<IEnumerable<Film>> GetAllContentAsync();
    }
}
