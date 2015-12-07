using DJO.PoolLeague.Models;
using DJO.PoolLeague.PointsCalculators;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Logging;
using System;
using System.Collections.Generic;

namespace DJO.PoolLeague.Services
{
    public interface IPointsManager : IDependency
    {
        int CalculatePoints(PoolGame poolGame);
        void UpdateParticipantScores(PoolGame poolGame);
    }

    public class PointsManager : IPointsManager
    {
        private readonly IContentManager _contentManager;
        private readonly IEnumerable<IPointsCalculator> _pointsCalculators;

        public PointsManager(IContentManager contentManager, IEnumerable<IPointsCalculator> pointsCalculators)
        {
            _contentManager = contentManager;
            _pointsCalculators = pointsCalculators;
             Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public int CalculatePoints(PoolGame poolGame)
        {
            _pointsCalculators.Invoke(x => x.CalculatePoints(poolGame), Logger);
            return poolGame.Points;
        }

        public void UpdateParticipantScores(PoolGame poolGame)
        {     
            var winner = poolGame.Winner;
            var loser = poolGame.Loser;

            if (winner == null)
                throw new ArgumentException("Cannot log a game against an inactive user", "winnerId");

            if (loser == null)
                throw new ArgumentException("Cannot log a game against an inactive user", "loserId");

            //update winner
            winner.Points = winner.Points + poolGame.Points;

            if (winner.Points > winner.HighestPoints)
                winner.HighestPoints = winner.Points;

            winner.WinStreak = winner.WinStreak < 0 ? 1 : winner.WinStreak + 1;

            if (winner.WinStreak > winner.BestWinStreak)
                winner.BestWinStreak = winner.WinStreak;

            winner.GamesWon = winner.GamesWon + 1;

            //update loser       
            loser.Points = loser.Points - poolGame.Points;
            loser.WinStreak = loser.WinStreak > 0 ? -1 : loser.WinStreak - 1;
            loser.GamesLost = loser.GamesLost + 1;
        }
    }
}