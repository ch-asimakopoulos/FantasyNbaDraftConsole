namespace FantasyNbaDraftConsole.Data.Dto
{
    using System.Collections.Generic;
    
    /// <summary>
    /// 
    /// </summary>
    public class PlayersInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Starred { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Ranking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<string> Positions { get; set; }
    }
}
