namespace FantasyNbaDraftConsole.Data.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectionTotal
    {
        /// <summary>
        /// 
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Player Player { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ProjectionValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Constants.ProjectionTotalsTypeId ProjectionTotalsTypeId { get; set; }
    }
}
