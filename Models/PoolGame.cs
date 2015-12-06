using Orchard.ContentManagement;
using System.Linq;

namespace DJO.PoolLeague.Models
{
    public class PoolGame
    {
        private readonly IContentManager _contentManager;

        public PoolGame(IContentManager contentManager, int winnerId, int loserId, WonBy wonby)
        {
            _contentManager = contentManager;
            SetGameParticipants(winnerId, loserId);
            WonBy = wonby;
        }

        public PoolParticipantPart Winner { get; set; }
        public PoolParticipantPart Loser { get; set; }
        public WonBy WonBy { get; set; }         
        public int Points { get; set; }    

        private void SetGameParticipants(int winnerId, int loserId)
        {
            var participants = _contentManager.GetMany<PoolParticipantPart>(new int[] { winnerId, loserId }, VersionOptions.Published, QueryHints.Empty);

            Winner = participants.FirstOrDefault(x => x.Id == winnerId && x.Active);
            Loser = participants.FirstOrDefault(x => x.Id == loserId && x.Active);
        }
    }
}