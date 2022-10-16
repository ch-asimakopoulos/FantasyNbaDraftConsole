namespace FantasyNbaDraftConsole
{
    using System.Collections.Generic;
    
    /// <summary>
    /// 
    /// </summary>
    public interface IHelperMapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public IEnumerable<Data.Models.Position> MapToPositionModel(string[] positions);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public Data.Models.Player MapRowToPlayerModel(Csv.ImportPlayers.ImportPlayersRow row);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projections"></param>
        /// <returns></returns>
        public ICollection<Constants.ProjectionTypeId> MapToProjectionTypeId(string[] projections);
    }
}