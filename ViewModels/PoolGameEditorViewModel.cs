using DJO.PoolLeague.Models;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DJO.PoolLeague.ViewModels
{
    public class PoolGameEditorViewModel
    {
        public int WinnerId { get; set; }
        public int LoserId { get; set; }
        public WonBy WonBy { get; set; }
        public IEnumerable<dynamic> Participants { get; set; }
    }
}