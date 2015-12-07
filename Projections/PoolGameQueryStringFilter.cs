using DJO.PoolLeague.Models;
using Orchard;
using Orchard.Localization;
using Orchard.Projections.Descriptors.Filter;
using Orchard.Projections.Services;

namespace DJO.PoolLeague.Projections
{
    public class PoolGamesQueryStringFilter : IFilterProvider
    {
        private readonly IWorkContextAccessor _wca;

        public PoolGamesQueryStringFilter(IWorkContextAccessor wca)
        {
            _wca = wca;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeFilterContext describe)
        {
            describe.For("PoolGamePartRecord", T("Pool Game Part Record"), T("Pool Game Part Record"))
                .Element("ParticipantQueryString", T("Partipant Query String"), T("Select pool games for participant e.g. \"?id=1\""),
                    ApplyFilter,
                    context => T("Select pool participants that are active")
                );
        }

        public void ApplyFilter(FilterContext context)
        {
            var workContext = _wca.GetContext();

            if (workContext == null || workContext.HttpContext == null || workContext.HttpContext.Request == null)
                return;

            var querystring = workContext.HttpContext.Request.QueryString["id"];

            int participantId;
            if (!int.TryParse(querystring, out participantId))
                return;

            context.Query.Where(alias => alias.ContentPartRecord<PoolGamePartRecord>(), factory => factory.Or(lhs => lhs.Eq("WinnerId", participantId), rhs => rhs.Eq("LoserId", participantId)));
        }
    }
}