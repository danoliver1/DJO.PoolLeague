using Orchard.ContentManagement;

namespace DJO.PoolLeague.Models
{
    public class PoolParticipantPart : ContentPart<PoolParticipantPartRecord>
    {
        public int ParticipantId
        {
            get
            {
                return Record.Id;
            }
        }

        public string DisplayName
        {
            get
            {
                return Record.DisplayName;
            }
            set
            {
                Record.DisplayName = value;
            }
        }

        public bool Active
        {
            get
            {
                return Record.Active;
            }
            set
            {
                Record.Active = value;
            }
        }

        public int BestWinStreak
        {
            get
            {
                return Record.BestWinStreak;
            }
            set
            {
                Record.BestWinStreak = value;
            }
        }

        public int HighestPoints
        {
            get
            {
                return Record.HighestPoints;
            }
            set
            {
                Record.HighestPoints = value;
            }
        }

        public int GamesWon
        {
            get
            {
                return Record.GamesWon;
            }
            set
            {
                Record.GamesWon = value;
            }
        }

        public int GamesLost
        {
            get
            {
                return Record.GamesLost;
            }
            set
            {
                Record.GamesLost = value;
            }
        }

        public float WinPercentage
        {
            get
            {
                if (GamesWon == 0 && GamesLost == 0)
                    return 1f;

                if (GamesWon == 0 && GamesLost > 0)
                    return 0f;

                return (float)(GamesWon) / (GamesWon + GamesLost);
            }
        } 

        public int WinStreak
        {
            get
            {
                return Record.WinStreak;
            }
            set
            {
                Record.WinStreak = value;
            }
        }

        public int Points
        {
            get
            {
                return Record.Points;
            }
            set
            {
                Record.Points = value;
            }
        }

    }
}