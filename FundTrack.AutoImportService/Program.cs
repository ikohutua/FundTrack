using FundTrack.AutoImportService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace FundTrack.AutoImportService
{
    class Program
    {        
        static void Main(string[] args)
        {
            AutoImportIntervalService service = new AutoImportIntervalService();
            service.InitializeTimers();
            while (true) ;
        }
    }   
}
