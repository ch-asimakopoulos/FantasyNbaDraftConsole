using System.Collections.Generic;

namespace FantasyNbaDraftConsole
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOperationsService
    {
        /// <summary>
        /// 
        /// </summary>
        public void SeedDatabase();

        /// <summary>
        /// 
        /// </summary>
        public void ListDraftPositions();

        /// <summary>
        /// 
        /// </summary>
        public bool StarPlayer(string name);

        /// <summary>
        /// 
        /// </summary>
        public void InitializeDraftPositions();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public int SeedPlayers(string filePath);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Data.Dto.DraftInfo GetDraftInfo(string leagueName);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICollection<Data.Dto.LiveRankingsInfo> GetLiveRankings();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="teamId"></param>
        /// <param name="pickNumber"></param>
        /// <returns></returns>
        public bool PickPlayer(string name, byte teamId, short? pickNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="projectionToPunt"></param>
        /// <returns></returns>
        public ICollection<Data.Dto.PlayersInfo> GetBestAvailablePlayers(int? number, ICollection<Constants.ProjectionTypeId> projectionToPunt);
    }
}