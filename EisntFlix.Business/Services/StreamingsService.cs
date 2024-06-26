﻿using EisntFlix.Business.Services.IServices;
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
    public class StreamingsService : EntityBaseRepository<Streaming>, IStreamingsService
    {
        public StreamingsService(AppDbContext context) : base(context)
        {
        }
    }
}
