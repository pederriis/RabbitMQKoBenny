using System;
using MasterData.Dto;

namespace MasterData.Storage
{
    public interface IStorage
    {
        /// <summary>
        /// Saves a cows data to the database
        /// </summary>
        /// <param name="earTag">Ear tag on the cow</param>
        /// <param name="name">The name of the cow</param>
        /// <param name="birthday">The birthday of the cow</param>
        void Store(string earTag, string name, DateTime birthday);

        /// <summary>
        /// Read the cows data
        /// </summary>
        /// <param name="earTag">Ear tag</param>
        /// <returns>A tuble containing the cows name and birthday</returns>
        (string Name, DateTime Birthday) Read(string earTag);
    }
}