using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Net;

namespace PCRProcess
{
    public class FilmCinema
    {
        //Properties for Output
        public string FilmName;
        public string CinemaName;
        public string CinemaCode;
        public string Date;
        public string Time;
        public string Link;
        public string Splitter;
        public string Type;
        //Properties for Local Process
        public int Start;
        public int End;
        public int Seed;
        public string CinemaLink;
        public string WebLink;
        public string FilePath;
        public string EmptyRow;


        public string RemoveCarrieReturn(string value)
        {
            char[] carrie = new char[] { '\r', '\n','\t' };
            return value.TrimStart(carrie).TrimEnd(carrie);
        }

        public void WriteToCSV(ArrayList value)
        {
            using (StreamWriter wr = new StreamWriter(FilePath + DateTime.Today.ToString("yyyyMMdd") + "-" + Type + ".txt", true,Encoding.Unicode  ))
            {
                for (int i = 0; i < value.Count; i++)
                {
                    wr.WriteLine(value[i]);
                }
             
            }
        }
        public string LoadFromFile(string file)
        {
            string value = new StreamReader(file).ReadToEnd();
            return value;
        }
        public string LoadFromWeb(string url)
        {
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8 ;     
            string value = client.DownloadString(url);
            return value;
        }
        public FilmCinema()
        {
            Splitter = "|";
            EmptyRow = "                                                                  ";
            EmptyRow += EmptyRow;
            EmptyRow += EmptyRow;
            EmptyRow += EmptyRow;
            EmptyRow += EmptyRow;
        }
        public void LoadConfig(PCRConfig config)
        {
            CinemaName = config.CinemaName;
            CinemaCode = config.CinemaCode;
            CinemaLink = config.Link;
            WebLink = config.Web;
            Type = config.Type;    
          
        }
    }
}
