using System;
using System.Threading;
using cowsumer3.Logic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace cowsumer3
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Thread StaticCallerLocation = new Thread(
            new ThreadStart(Subscriber.ListentoCowLocation));

            Thread StaticCallerCow = new Thread(
           new ThreadStart(Subscriber.ListentoCowCreation));

            // Start the thread.
            StaticCallerLocation.Start();
            StaticCallerCow.Start();

            BuildWebHost(args).Run();
            
        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:10003/")
                .Build();

        
    }
}

