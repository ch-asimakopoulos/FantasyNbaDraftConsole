namespace FantasyNbaDraftConsole.Data.Dto
{
    using System.Collections.Generic;
    
    /// <summary>
    /// 
    /// </summary>
    public class BestPlayersInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Ranking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Positions { get; set; }
    }
}
