using DJO.PoolLeague.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Projections.Services;
using Orchard.Projections.Descriptors.SortCriterion;

namespace DJO.PoolLeague.Projections
{
    public class PoolParticipantPointsSortCriterion : Component, ISortCriterionProvider
    {
        public void Describe(DescribeSortCriterionContext describe)
        {
            describe.For("PoolParticipantPartRecord", new LocalizedString("Pool Participant Part Record"), T("Pool participant ordering"))
                .Element("Points", T("Points"), T("Sorts the results by points"),
                context => context.Query.OrderBy(alias => alias.ContentPartRecord<PoolParticipantPartRecord>(), x => x.Desc("Points")),
                context => T("Ordered by pool participant's points")
            );
        }
    }
}