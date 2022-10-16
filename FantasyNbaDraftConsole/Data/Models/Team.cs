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
        public string Name { get; set; }

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
        public byte DraftPosition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual League League { get; set; }
         
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
