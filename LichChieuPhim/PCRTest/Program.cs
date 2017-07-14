using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections;
using System.IO;
using PCRProcess;

namespace PCRConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PCRProcess.PCRProcess pcr = new PCRProcess.PCRProcess();
            pcr.LoadConfig("Config.xml");
            pcr.ProcessFilmSchedule();  
        }    
    }
}
