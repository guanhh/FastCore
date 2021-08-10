﻿using FastCore.EFCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FastCore.DbMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).RunConsoleAsync();
            Console.ReadKey();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureServices((context, services) =>
              {
                  var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                        .Build();

                  var connectionString = config.GetConnectionString("FastCoreConnection");
                  services.AddDbContext<FastCoreContext>(options => options.UseSqlServer(connectionString));

                  services.AddHostedService<DbMigratorHostedService>();
              });

    }
}