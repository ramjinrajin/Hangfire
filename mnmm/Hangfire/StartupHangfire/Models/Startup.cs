using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.IO;
using Hangfire;
using System.Configuration;
using System.Data.SqlClient;

[assembly: OwinStartup(typeof(StartupHangfire.Models.Startup))]

namespace StartupHangfire.Models
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HangFireInBuildDb"].ConnectionString);
            sqlConnection.Open();

            GlobalConfiguration.Configuration.UseSqlServerStorage(ConfigurationManager.ConnectionStrings["HangFireInBuildDb"].ConnectionString);

            BackgroundJob.Enqueue(() => Console.WriteLine("Getting Started with HangFire!"));

            BackgroundJob.Schedule(() => Console.WriteLine("This background job would execute after a delay."), TimeSpan.FromMilliseconds(1000));

            RecurringJob.AddOrUpdate(() => FileWriter(), Cron.Minutely);

            app.UseHangfireDashboard();

            app.UseHangfireServer();
        }

        public void FileWriter()
        {
            Guid guid;
            guid = Guid.NewGuid();
            System.IO.File.Create(AppDomain.CurrentDomain.BaseDirectory + guid + ".txt");
        }
    }
}
