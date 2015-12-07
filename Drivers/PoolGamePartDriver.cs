using DJO.PoolLeague.Models;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Security;
using DJO.PoolLeague.ViewModels;
using Orchard.Services;
using DJO.PoolLeague.Services;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;

namespace DJO.PoolLeague.Drivers
{
    public class PoolGamePartDriver : ContentPartDriver<PoolGamePart>
    {
        private readonly IContentManager _contentManager;
        private readonly IClock _clock;
        private readonly IPointsManager _pointsManager;

        public PoolGamePartDriver(IContentManager contentManager, IClock clock, IPointsManager pointsManager)
        {
            _contentManager = contentManager;
            _clock = clock;
            _pointsManager = pointsManager;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(PoolGamePart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape(
                "Parts_PoolGame",
                () => {
                    var participants = _contentManager.GetMany<PoolParticipantPart>(new int[] { part.WinnerId, part.LoserId }, VersionOptions.Latest, QueryHints.Empty);
                    return shapeHelper.Parts_PoolGame(                        
                        Winner: participants.FirstOrDefault(x => x.Id == part.WinnerId),
                        Loser: participants.FirstOrDefault(x => x.Id == part.LoserId),
                        Part: part);
                });
        }

        protected override DriverResult Editor(PoolGamePart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_PoolGame_Edit",
                () =>
                {
                    var participants = _contentManager
                        .Query(VersionOptions.Published)
                        .Where<PoolParticipantPartRecord>(x => x.Active)
                        .List()
                        .ToList();

                    return shapeHelper.EditorTemplate(
                        TemplateName: "Parts/PoolGame",
                        Model: new PoolGameEditorViewModel { WinnerId = part.WinnerId, LoserId = part.LoserId, WonBy = part.WonBy, Participants = participants },
                        Prefix: Prefix);
                });
        }


        protected override DriverResult Editor(PoolGamePart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var model = new PoolGameEditorViewModel();
            updater.TryUpdateModel(model, Prefix, null, null);

            if (model.WinnerId == model.LoserId)
                updater.AddModelError("WinnerIsLoser", T("Winner and Loser cannot be the same"));
           

            part.WinnerId = model.WinnerId;
            part.LoserId = model.LoserId;
            part.WonBy = model.WonBy;

            var poolGame = new PoolGame(_contentManager, model.WinnerId, model.LoserId, model.WonBy);
            part.Points = _pointsManager.CalculatePoints(poolGame);
            _pointsManager.UpdateParticipantScores(poolGame);

            return Editor(part, shapeHelper);
        }

        protected override void Importing(PoolGamePart part, ImportContentContext context)
        {
            // Don't do anything if the tag is not specified.
            if (context.Data.Element(part.PartDefinition.Name) == null)
            {
                return;
            }

            part.WinnerId = Convert.ToInt32(context.Attribute(part.PartDefinition.Name, "WinnerId"));
            part.LoserId = Convert.ToInt32(context.Attribute(part.PartDefinition.Name, "LoserId"));
            part.WonBy = (WonBy)Enum.Parse(typeof(WonBy), context.Attribute(part.PartDefinition.Name, "WonBy"));
            part.Points = Convert.ToInt32(context.Attribute(part.PartDefinition.Name, "Points"));
        }

        protected override void Exporting(PoolGamePart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("WinnerId", part.WinnerId);
            context.Element(part.PartDefinition.Name).SetAttributeValue("LoserId", part.LoserId);
            context.Element(part.PartDefinition.Name).SetAttributeValue("WonBy", part.WonBy);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Points", part.Points);
        }
    }
}