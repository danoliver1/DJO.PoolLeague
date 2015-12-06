using Orchard.Projections.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Projections.Descriptors.Filter;
using Orchard.Localization;
using DJO.PoolLeague.Models;

namespace DJO.PoolLeague.Projections
{
    public class PoolParticipantActiveFilter : IFilterProvider
    {
        public PoolParticipantActiveFilter()
        {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeFilterContext describe)
        {
            describe.For("PoolParticipantPartRecord", T("Pool Participant Part Record"), T("Pool Participant Part Record"))
                .Element("Active", T("Participant Active"), T("Select pool participants that are active"),
                    context => context.Query.Where(alias => alias.ContentPartRecord<PoolParticipantPartRecord>(), x => x.Eq("Active", true)),
                    context => T("Select pool participants that are active")
                );
        }
    }
}