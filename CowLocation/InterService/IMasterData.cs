namespace CowLocation.InterService
{
    public interface IMasterData
    {
        /// <summary>
        /// Contacts MasterData service to retrieve name on a specific ear tag
        /// </summary>
        /// <param name="earTag">Cow ear tag</param>
        /// <returns>Cows name</returns>
        string GetCowName(string earTag);
    }
}
