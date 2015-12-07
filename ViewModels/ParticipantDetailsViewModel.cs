using DJO.PoolLeague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DJO.PoolLeague.ViewModels
{
    public class ParticipantDetailsViewModel
    {
        public IList<Opponent> Opponents { get; set; }
        public PoolParticipantPart Participant { get; set; }

        public class Opponent
        {
            public string DisplayName { get; set; }
            public int Wins { get; set; }
            public int Losses { get; set; }
            public int Points { get; set; }
            public float WinPercentage
            {
                get
                {
                    if (Wins == 0 && Losses > 0)
                        return 0f;

                    return (float)(Wins) / (Wins + Losses);
                }
            }
        }
    }
}