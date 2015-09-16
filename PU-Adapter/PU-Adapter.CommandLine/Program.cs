﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Kentor.PU_Adapter.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var fetcher = new PU_Adapter.PknodFetcher();
            //Change SSL checks so that all checks pass
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });

            while (true)
            {
                Console.WriteLine("Enter person number");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    return;
                }
                var result = fetcher.FetchPkNodplusString(input);
                Console.WriteLine(result);
                Console.WriteLine("----------------------------------------------");
                var parsedData = new PknodPlusData(result);
                var json = JsonConvert.SerializeObject(parsedData, Formatting.Indented);
                Console.WriteLine(json);
                Console.WriteLine("----------------------------------------------");
            }
        }
    }
}
