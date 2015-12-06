using Orchard.ContentManagement.Records;

namespace DJO.PoolLeague.Models
{
    public class PoolParticipantPartRecord : ContentPartRecord
    {
        public virtual string DisplayName { get; set; }

        public virtual bool Active { get; set; }

        public virtual int BestWinStreak { get; set; }

        public virtual int HighestPoints { get; set; }

        public virtual int GamesWon { get; set; }

        public virtual int GamesLost { get; set; }

        public virtual int WinStreak { get; set; }

        public virtual int Points { get; set; }
    }
}