using Orchard.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DJO.PoolLeague
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            manifest.DefineScript("RankLeague").SetUrl("rankleague.js").SetDependencies("jQuery");
            manifest.DefineStyle("PoolLeague").SetUrl("PoolLeague.css");
        }
    }
}