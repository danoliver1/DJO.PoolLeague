using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DJO.PoolLeague
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable("PoolParticipantPartRecord", table => table
                .ContentPartRecord()
                .Column<string>("DisplayName")
                .Column<bool>("Active")                
                .Column<int>("BestWinStreak", col => col.WithDefault(0))
                .Column<int>("HighestPoints", col => col.WithDefault(1000))
                .Column<int>("GamesWon", col => col.WithDefault(0))
                .Column<int>("GamesLost", col => col.WithDefault(0))
                .Column<int>("WinStreak", col => col.WithDefault(0))
                .Column<int>("Points", col => col.WithDefault(1000))
            );

            ContentDefinitionManager.AlterTypeDefinition("User", b => b
                .WithPart("PoolParticipantPart")
            );

            SchemaBuilder.CreateTable("PoolGamePartRecord", table => table
                .ContentPartRecord()
                .Column<int>("WinnerId")
                .Column<int>("LoserId")
                .Column<int>("Points")
                .Column<string>("WonBy")
                .Column<DateTime>("Date")
            );

            ContentDefinitionManager.AlterTypeDefinition("PoolGame", b => b
                .WithPart("PoolGamePart")
                .WithPart("CommonPart") // uesd by projection, implementing a SortCriterion class that uses PoolGamePart.Date would make this obselete
            );

            return 1;
        }
    }
}