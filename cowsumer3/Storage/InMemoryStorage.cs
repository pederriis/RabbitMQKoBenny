using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace cowsumer3.Storage
{
    /// <summary>
    /// Concern: To handle data in the applications uptime
    /// </summary>
    public class InMemoryStorage : IStorage
    {
        // Simulate table in database
        private static Dictionary<string, List<LocationData>> _dataStore = new Dictionary<string, List<LocationData>>();

        private static Dictionary<string, CowData> _dataStoreCow = new Dictionary<string, CowData>();


        public List<LocationData> LocationRead(string earTag)
        {
            //noget kode der henter cowlocation op fra databasen her

            var locations = _dataStore.FirstOrDefault(pair => pair.Key == earTag).Value;

            return locations;
            //return (_dataStore[earTag].Latitude, _dataStore[earTag].Longitude);
        }

        public void LocationCreateUpdate(string earTag, double latitude, double longitude)
        {
            if (_dataStore.ContainsKey(earTag))
            {
                _dataStore[earTag].Add(new LocationData { Latitude = latitude, Longitude = longitude });
               // _dataStore[earTag].Longitude = longitude;
            }
            else
            {
                List<LocationData> newLocations = new List<LocationData>();
                newLocations.Add(new LocationData { Latitude = latitude, Longitude = longitude });
                _dataStore.Add(earTag, newLocations);


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
       
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
