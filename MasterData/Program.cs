﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MasterData
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:10002/")
                .Build();
    }
}
