namespace CowLocation.Storage
{
    public interface IStorage
    {
        /// <summary>
        /// Reads GPS coordinates matching ear tag
        /// </summary>
        /// <param name="earTag">Ear tag</param>
        /// <returns>Latitude and Longitude packed in a tuble</returns>
        (double Latitude, double Longitude) LocationRead(string earTag);

        /// <summary>
        /// Writes newest location on a cow, to the database
        /// </summary>
        /// <param name="earTag">Ear tag</param>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        void LocationCreateUpdate(string earTag, double latitude, double longitude);
    }
}
