using EisntFlix.Data.Access.BaseRepository;
using EisntFlix.Models.ViewsModel;
using EisntFlix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Business.Services.IServices
{
    public interface ISeriesService : IEntityBaseRepository<Serie>
    {
        Task<Serie> GetSerieByIdAsync(int id);
        Task<NewFilmDropdownsVM> GetNewSerieDropdownsValues();

        Task AddNewSerieAsync(NewSerieVM data);

        Task UpdateSerieAsync(NewSerieVM data);
    }
}