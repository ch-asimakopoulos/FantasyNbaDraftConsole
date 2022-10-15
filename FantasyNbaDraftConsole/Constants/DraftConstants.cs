using System.Collections.Generic;

namespace FantasyNbaDraftConsole.Constants
{
    /// <summary>
    /// 
    /// </summary>
    public static class DraftConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public const decimal PuntCutoff = -1.6M;

        /// <summary>
        /// 
        /// </summary>
        public const string LeagueName = "ClashifiedLeague";

        /// <summary>
        /// 
        /// </summary>
        public static readonly IReadOnlySet<string> DefaultPositions = new HashSet<string>
        {
            "PG", "SG", "SF", "PF", "C", "F", "G"
        };
    }
}
