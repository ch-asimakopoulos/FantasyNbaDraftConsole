namespace FantasyNbaDraftConsole.Data.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Team
    {
        /// <summary>
        /// 
        /// </summary>
        public byte TeamId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte LeagueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual League League { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte TeamDraftPosition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual List<Player> DraftedPlayers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Team()
        {
            DraftedPlayers = new List<Player>();
        }
    }
}
