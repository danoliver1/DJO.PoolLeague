using DJO.PoolLeague.Models;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Security;
using DJO.PoolLeague.ViewModels;
using Orchard.ContentManagement.Handlers;

namespace DJO.PoolLeague.Drivers
{
    public class PoolParticipantPartDriver : ContentPartDriver<PoolParticipantPart>
    {
        protected override DriverResult Display(PoolParticipantPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_PoolParticipant", () => shapeHelper.Parts_PoolParticipant(Part: part));
        }

        protected override DriverResult Editor(PoolParticipantPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_PoolParticipant_Edit",
                () => shapeHelper.EditorTemplate(
                        TemplateName: "Parts/PoolParticipant",
                        Model: part,
                        Prefix: Prefix));
        }


        protected override DriverResult Editor(PoolParticipantPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }

        protected override void Importing(PoolParticipantPart part, ImportContentContext context)
        {
            // Don't do anything if the tag is not specified.
            if (context.Data.Element(part.PartDefinition.Name) == null)
            {
                return;
            }

            part.DisplayName = context.Attribute(part.PartDefinition.Name, "DisplayName");
            part.Active = bool.Parse(context.Attribute(part.PartDefinition.Name, "Active"));
            part.BestWinStreak = Convert.ToInt32(context.Attribute(part.PartDefinition.Name, "BestWinStreak"));
            part.HighestPoints = Convert.ToInt32(context.Attribute(part.PartDefinition.Name, "HighestPoints"));
            part.GamesWon = Convert.ToInt32(context.Attribute(part.PartDefinition.Name, "GamesWon"));
            part.GamesLost = Convert.ToInt32(context.Attribute(part.PartDefinition.Name, "GamesLost"));
            part.WinStreak = Convert.ToInt32(context.Attribute(part.PartDefinition.Name, "WinStreak"));
            part.Points = Convert.ToInt32(context.Attribute(part.PartDefinition.Name, "Points"));
        }

        protected override void Exporting(PoolParticipantPart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("DisplayName", part.DisplayName);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Active", part.Active);
            context.Element(part.PartDefinition.Name).SetAttributeValue("BestWinStreak", part.BestWinStreak);
            context.Element(part.PartDefinition.Name).SetAttributeValue("HighestPoints", part.HighestPoints);
            context.Element(part.PartDefinition.Name).SetAttributeValue("GamesWon", part.GamesWon);
            context.Element(part.PartDefinition.Name).SetAttributeValue("GamesLost", part.GamesLost);
            context.Element(part.PartDefinition.Name).SetAttributeValue("WinStreak", part.WinStreak);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Points", part.Points);
        }
    }
}