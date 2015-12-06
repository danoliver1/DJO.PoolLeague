using DJO.PoolLeague.Models;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DJO.PoolLeague.PointsCalculators
{
    public interface IPointsCalculator : IDependency
    {
        void CalculatePoints(PoolGame poolGame);
    }
}