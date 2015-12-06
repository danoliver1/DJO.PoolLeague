using System;
using System.Collections.Generic;
using System.Linq;
using DJO.PoolLeague.Models;
using Orchard.ContentManagement;
using Orchard.Projections.Services;
using Orchard.Projections.Models;
using Orchard.Core.Title.Models;
using Orchard.Environment.Extensions;

namespace DJO.PoolLeague.PointsCalculators
{
    [OrchardFeature("DJO.PoolLeague.Points.RankDifference")]
    public class RankDifferencePointsCalculator : IPointsCalculator
    {
        private readonly IContentManager _contentManager;
        private readonly IProjectionManager _projectionManager;

        private List<ContentItem> _league;

        public RankDifferencePointsCalculator(IContentManager contentManager, IProjectionManager projectionManager)
        {
            _contentManager = contentManager;
            _projectionManager = projectionManager;
        }

        public void CalculatePoints(PoolGame poolGame)
        {
            var winnerRank = GetRank(poolGame.Winner.Id);
            var loserRank = GetRank(poolGame.Loser.Id);

            var rankDifference = winnerRank - loserRank;

            if (rankDifference > 15)
                poolGame.Points = poolGame.Points + 8;
            else if (rankDifference > 10)
                poolGame.Points = poolGame.Points + 6;
            else if (rankDifference > 5)
                poolGame.Points = poolGame.Points + 3;
            else if (rankDifference > -2)
                poolGame.Points = poolGame.Points + 2;
        }
        
        private int GetRank(int id)
        {
            if (_league == null)
                _league = GetLeague();

            for (var i = 0; i < _league.Count; i++)
                if (_league[i].Id == id)
                    return i + 1;

            return 0;
        }
        
        private List<ContentItem> GetLeague()
        {
            var query = _contentManager.Query<QueryPart>()
                .Where<TitlePartRecord>(x => x.Title == "Pool League")
                .List()
                .FirstOrDefault();

            //.Invoke will catch and log below exception
            if (query == null)
                throw new NullReferenceException("Cannot calculate league because no query was found with name 'Pool League'");

            return _projectionManager.GetContentItems(query.Id).ToList();
        }
    }
}