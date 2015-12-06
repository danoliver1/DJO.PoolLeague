using Orchard.ContentManagement.Records;
using Orchard.Security;
using System;

namespace DJO.PoolLeague.Models
{
    public class PoolGamePartRecord : ContentPartRecord
    {
        public virtual int WinnerId { get; set; }
        public virtual int LoserId { get; set; }
        public virtual int Points { get; set; }
        public virtual WonBy WonBy { get; set; }
        public virtual DateTime Date { get; set; }
    }
}