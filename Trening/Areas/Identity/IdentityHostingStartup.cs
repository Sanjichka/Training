using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trening.Areas.Identity.Data;
using Trening.Data;

[assembly: HostingStartup(typeof(Trening.Areas.Identity.IdentityHostingStartup))]
namespace Trening.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TreningContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TreningContext")));

            });
        }
    }
}