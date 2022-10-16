namespace FantasyNbaDraftConsole.Data.Dto
{
    using System.Collections.Generic;
    public class LiveRankingsInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<PlayersInfo> PlayersDrafted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<Constants.ProjectionTypeId, decimal> ProjectionTypeRankings { get; set; }
    }
}
