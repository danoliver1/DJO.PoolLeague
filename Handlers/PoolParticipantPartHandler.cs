using DJO.PoolLeague.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace DJO.PoolLeague.Handlers
{
    public class PoolParticipantPartHandler : ContentHandler
    {
        public PoolParticipantPartHandler(IRepository<PoolParticipantPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));

            OnPublishing<PoolParticipantPart>((context, part) =>
            {
                if (part.GamesWon == 0 && part.GamesLost == 0)
                {
                    part.Points = 1000;
                    part.HighestPoints = 1000;
                }
            }
            );
        }
    }
}