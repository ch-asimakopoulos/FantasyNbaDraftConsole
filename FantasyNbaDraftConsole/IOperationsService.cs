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
        public void InitializeDraftPositions();
        
        /// <summary>
        /// 
        /// <param name="filePath"></param>
        /// </summary>
        /// <returns></returns>
        public int SeedPlayers(string filePath);
    }
}