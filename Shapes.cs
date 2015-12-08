using DJO.PoolLeague.Services;
using Orchard;
using Orchard.DisplayManagement;
using Orchard.Environment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DJO.PoolLeague
{
    public class Shapes : IDependency
    {
        private readonly Work<IPoolPlayerSummary> _poolPlayerSummary;
        private readonly Work<WorkContext> _workContext;
        private readonly Work<IShapeFactory> _shapeFactory;

        public Shapes(Work<IPoolPlayerSummary> poolPlayerSummary, Work<WorkContext> workContext, Work<IShapeFactory> shapeFactory)
        {
            _poolPlayerSummary = poolPlayerSummary;
            _workContext = workContext;
            _shapeFactory = shapeFactory;
        }

        protected dynamic New {
            get { return _shapeFactory.Value; }
        }

        [Shape]
        public void PoolParticipantDetails(dynamic Display, TextWriter Output, HtmlHelper Html)
        {
            var id = _workContext.Value.HttpContext.Request.QueryString["id"];

            int participantId;
            if (!int.TryParse(id, out participantId))
            {
                Output.Write("Invalid id provided");
                return;
            }

            var model = _poolPlayerSummary.Value.GetPlayerSummary(participantId);
            if(model == null || model.Participant == null)
            {
                Output.Write("Invalid id provided");
                return;
            }

            Output.WriteLine(Display.PoolParticipantDetails_Stats(Participant: model.Participant));
            Output.WriteLine(Display.PoolParticipantDetails_Table(Opponents: model.Opponents));
        }
    }
}