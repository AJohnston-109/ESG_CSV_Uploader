﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ESGCsvUploader
{
    public static class AppConfig
    {
        private static IConfiguration _iconfiguration;
        static AppConfig()
        {
            GetAppSettingsFile();
        }

        public static void GetAppSettingsFile()
        {

            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();

            //return _iconfiguration;
        }

        public static string GetConnectionString()
        {
            return _iconfiguration.GetConnectionString("Default");
        }
    }
}
