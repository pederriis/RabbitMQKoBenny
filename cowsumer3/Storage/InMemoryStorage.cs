using System;
using System.Collections.Generic;

namespace cowsumer3.Storage
{
    /// <summary>
    /// Concern: To handle data in the applications uptime
    /// </summary>
    public class InMemoryStorage : IStorage
    {
        // Simulate table in database
        private static Dictionary<string, LocationData> _dataStore = new Dictionary<string, LocationData>();

        private static Dictionary<string, CowData> _dataStoreCow = new Dictionary<string, CowData>();


        public (double Latitude, double Longitude) LocationRead(string earTag)
        {
            //noget kode der henter cowlocation op fra databasen her
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

        public void StoreCow(string earTag, string name, DateTime birthday)
        {
            if (_dataStoreCow.ContainsKey(earTag))
                throw new Exception("EarTag already exists");

            _dataStoreCow.Add(earTag, new CowData
            {
                Name = name,
                Birthday = birthday,
                EarTag = earTag,
                CreateTimestamp = DateTime.Now
            });
        }

        public (string Name, DateTime Birthday) ReadCow(string earTag)
        {
            return (_dataStoreCow[earTag].Name, _dataStoreCow[earTag].Birthday);
        }
    }

    public class CowData
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string EarTag { get; set; }
        public DateTime CreateTimestamp { get; set; }
    }


    // Simulate table content in database
    public class LocationData
    {
        public string EarTag { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
