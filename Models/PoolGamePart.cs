using Orchard.ContentManagement;
using Orchard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DJO.PoolLeague.Models
{  
    public class PoolGamePart : ContentPart<PoolGamePartRecord>
    {
        public int WinnerId {
            get { return Record == null ? 0 : Record.WinnerId; }
            set { Record.WinnerId = value; }
        }

        public int LoserId {
            get { return Record == null ? 0 : Record.LoserId; }
            set { Record.LoserId = value; }
        }

        public int Points {
            get { return Record == null ? 0 : Record.Points; }
            set { Record.Points = value; }
        }

        public WonBy WonBy {
            get { return Record.WonBy; }
            set { Record.WonBy = value; }
        }

        public DateTime Date {
            get { return Record == null ? DateTime.MinValue : Record.Date; }
            set { Record.Date = value; }
        }
    }
}