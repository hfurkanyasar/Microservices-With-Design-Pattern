using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Payment.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
     Host.CreateDefaultBuilder(args)
         .ConfigureWebHostDefaults(webBuilder =>
         {
             webBuilder.UseStartup<Startup>();
         })
         .ConfigureLogging(logging =>
         {
             logging.ClearProviders(); // Mevcut g�nl�k sa�lay�c�lar�n� temizle
            logging.AddConsole(); // veya ba�ka bir g�nl�k sa�lay�c�s� ekle
        });

    }
}
