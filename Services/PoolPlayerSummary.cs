using DJO.PoolLeague.Models;
using DJO.PoolLeague.ViewModels;
using NHibernate.Transform;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DJO.PoolLeague.Services
{
    public interface IPoolPlayerSummary : IDependency
    {
        ParticipantDetailsViewModel GetPlayerSummary(int id);
    }
    public class PoolPlayerSummary : IPoolPlayerSummary
    {
        private readonly ITransactionManager _transactionManager;
        private readonly IContentManager _contentManager;

        public PoolPlayerSummary(ITransactionManager transactionManager, IContentManager contentManager)
        {
            _transactionManager = transactionManager;
            _contentManager = contentManager;
        }

        public ParticipantDetailsViewModel GetPlayerSummary(int id)
        {
            var query = @"             
               SELECT T.DisplayName, SUM(ISNULL(T.Wins, 0)) As Wins, SUM(ISNULL(T.Losses,0)) as Losses, SUM(ISNULL(T.Points,0)) as Points
                 FROM   (SELECT P.DisplayName, ISNULL(COUNT(*),0) as Wins, 0 as Losses, SUM(G.Points) as Points
                    FROM DJO_PoolLeague_PoolParticipantPartRecord P
                        INNER JOIN DJO_PoolLeague_PoolGamePartRecord G ON
                            P.Id = G.LoserId
                    WHERE G.WinnerId = :participantId
                    GROUP BY P.DisplayName, G.LoserId
                    UNION
                    SELECT P.DisplayName, 0 as Wins, ISNULL(COUNT(*),0) as Losses, SUM(G.Points) * -1 as Points
                    FROM DJO_PoolLeague_PoolParticipantPartRecord P
                        INNER JOIN DJO_PoolLeague_PoolGamePartRecord G ON
                            P.Id = G.LoserId
                    WHERE G.LoserId = :participantId
                    GROUP BY P.DisplayName, G.WinnerId) T
                GROUP BY
                    T.DisplayName";

            var result = _transactionManager.GetSession()
                .CreateSQLQuery(query)
                .SetParameter("participantId", id)
                .SetResultTransformer(Transformers.AliasToBean<ParticipantDetailsViewModel.Opponent>())
                .List<ParticipantDetailsViewModel.Opponent>()
                .OrderByDescending(x => x.WinPercentage)
                .ThenByDescending(x => x.Points)
                .ToList();

            return new ParticipantDetailsViewModel
            {
                Opponents = result,
                Participant = _contentManager.Get<PoolParticipantPart>(id)
            };
        }
    }
}
