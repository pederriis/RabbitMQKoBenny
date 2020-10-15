using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CowLocation
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
                .UseUrls("http://localhost:10001/")
                .Build();
    }
}
