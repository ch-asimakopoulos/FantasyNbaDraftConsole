namespace FantasyNbaDraftConsole.Data.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DraftConfig
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
        public byte LeagueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual League League { get; set; }
    }
}
