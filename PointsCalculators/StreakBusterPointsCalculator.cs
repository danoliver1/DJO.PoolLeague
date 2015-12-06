using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DJO.PoolLeague.Models;
using Orchard.Environment.Extensions;

namespace DJO.PoolLeague.PointsCalculators
{
    [OrchardFeature("DJO.PoolLeague.Points.StreakBuster")]
    public class StreakBusterPointsCalculator : IPointsCalculator
    {
        public void CalculatePoints(PoolGame poolGame)
        {
            if (poolGame.Loser.WinStreak >= 5)
                poolGame.Points = poolGame.Points + 1;
        }
    }
}