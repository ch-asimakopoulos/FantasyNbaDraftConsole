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
        /// <param name="filePath"></param>
        /// </summary>
        /// <returns></returns>
        public int SeedPlayers(string filePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<Data.Dto.BestPlayersInfo> GetBestAvailablePlayers(int? number);
    }
}