using System.Collections.Generic;

namespace CowLocation.Storage
{
    /// <summary>
    /// Concern: To handle data in the applications uptime
    /// </summary>
    public class InMemoryStorage : IStorage
    {
        // Simulate table in database
        private static Dictionary<string, LocationData> _dataStore = new Dictionary<string, LocationData>();

        public (double Latitude, double Longitude) LocationRead(string earTag)
        {
            return (_dataStore[earTag].Latitude, _dataStore[earTag].Longitude);
        }

        public void LocationCreateUpdate(string earTag, double latitude, double longitude)
        {
            if (_dataStore.ContainsKey(earTag))
            {
                _dataStore[earTag].Latitude = latitude;
                _dataStore[earTag].Longitude = longitude;
            }
            else
            {
                _dataStore.Add(earTag, new LocationData
                {
                    EarTag = earTag,
                    Latitude = latitude,
                    Longitude = longitude
                });
            }
        }
    }

    // Simulate table content in database
    public class LocationData
    {
        public string EarTag { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
