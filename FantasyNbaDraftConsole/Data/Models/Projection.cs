namespace FantasyNbaDraftConsole.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// 
    /// </summary>
    public class Projection
    {
        /// <summary>
        /// 
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ProjectionValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Constants.ProjectionTypeId ProjectionTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public bool Punted => ProjectionValue < Constants.DraftConstants.PuntCutoff;
    }
}
