using EisntFlix.Business.Services.IServices;
using EisntFlix.Data.Access.BaseRepository;
using EisntFlix.Data.Access.DbContext;
using EisntFlix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Business.Services
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context) { }
    }
}
