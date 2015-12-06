using DJO.PoolLeague.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DJO.PoolLeague.Handlers
{
    public class PoolGamePartHandler : ContentHandler
    {
        public PoolGamePartHandler(IRepository<PoolGamePartRecord> repository, IClock clock)
        {
            Filters.Add(StorageFilter.For(repository));
            OnInitializing<PoolGamePart>((context, part) => part.Date = clock.UtcNow);
        }
    }
}