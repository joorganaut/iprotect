using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AXAMansard.Framework.NHibernateManager.Configuration
{
    public enum DatabaseSource
    {
        /// <summary>
        /// Used for Databases on AXAMansard's end.
        /// </summary>
        Local = 0,
        /// <summary>
        /// Used for databases on the institutions' end.
        /// </summary>
        Remote,

        /// <summary>
        /// Another kind of databases on AXAMansards' end. A special type though.
        /// </summary>
        Core,

        Sequel
    }
}
