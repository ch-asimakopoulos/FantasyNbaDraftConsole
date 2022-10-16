using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace FantasyNbaDraftConsole.Data.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class DraftInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Snake { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Rounds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte TeamCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public (byte, string)[] TeamOrder { get; set; }
    }
}
