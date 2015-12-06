using DJO.PoolLeague.Models;
using Orchard.Environment.Extensions;

namespace DJO.PoolLeague.PointsCalculators
{
    [OrchardFeature("DJO.PoolLeague.Points.Default")]
    public class DefaultPointsCalculator : IPointsCalculator
    {
        public void CalculatePoints(PoolGame poolGame)
        {
            poolGame.Points = poolGame.Points + 2;
        }
    }
}