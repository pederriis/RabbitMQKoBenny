﻿using System;
using System.Collections.Generic;

namespace cowsumer3.Storage
{
    public interface IStorage
    {
        /// <summary>
        /// Reads GPS coordinates matching ear tag
        /// </summary>
        /// <param name="earTag">Ear tag</param>
        /// <returns>Latitude and Longitude packed in a tuble</returns>
       List<LocationData> LocationRead(string earTag);

        /// <summary>
        /// Writes newest location on a cow, to the database
        /// </summary>
        /// <param name="earTag">Ear tag</param>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        void LocationCreateUpdate(string earTag, double latitude, double longitude);
        (string Name, DateTime Birthday) ReadCow(string earTag);


    }
}
