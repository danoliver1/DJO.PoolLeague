using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DJO.PoolLeague.Models;

namespace DJO.PoolLeague.PointsCalculators
{
    [OrchardFeature("DJO.PoolLeague.Points.SevenBall")]
    public class WonByPointsCalculator : IPointsCalculator
    {
        public void CalculatePoints(PoolGame poolGame)
        {
            if (poolGame.WonBy == WonBy.SevenBalls)
                poolGame.Points = poolGame.Points + 2;
        }
    }
}