namespace FantasyNbaDraftConsole.Data.Models
{
    using System.Collections.Generic;
    
    /// <summary>
    /// 
    /// </summary>
    public class League
    {
        /// <summary>
        /// 
        /// </summary>
        public byte LeagueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LeagueName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool LeagueInitialized { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual List<Team> LeagueTeams { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DraftConfig DraftConfig { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public League()
        {
            LeagueTeams = new List<Team>();
        }
    }
}
