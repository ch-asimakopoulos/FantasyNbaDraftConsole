namespace FantasyNbaDraftConsole.Data.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public byte? TeamId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Starred { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Injured { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NbaTeam { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? PickNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Team Team { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AvailableToDraft => TeamId == null;

        /// <summary>
        /// 
        /// </summary>
        public virtual List<Position> Positions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual List<Projection> Projections { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual List<ProjectionTotal> ProjectionTotals { get; set; }

        public Player()
        {
            Positions = new List<Position>();
            Projections = new List<Projection>();
            ProjectionTotals = new List<ProjectionTotal>();
        }
    }
}