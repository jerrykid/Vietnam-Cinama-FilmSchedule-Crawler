using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;
namespace PCRProcess
{
    public class PCRProcess
    {
        public string FilePath;
        ArrayList Configs;
        public void LoadConfig(string filePath)
        {
            Configs = new ArrayList();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            //Load Cinema List
            XmlNodeList cinema = doc["config"].ChildNodes[0].ChildNodes;
            for (int i = 0; i < cinema.Count; i++)
            {
                PCRConfig config = new PCRConfig();
                config.CinemaCode = cinema[i].Attributes["CinemaCode"].Value;
                config.CinemaName = cinema[i].Attributes["CinemaName"].Value;
                config.Type = cinema[i].Attributes["Type"].Value;
                config.Link = cinema[i].Attributes["Link"].Value;
                config.Web = cinema[i].Attributes["Web"].Value;
                Configs.Add(config);   
            }
            //Load File Path
            FilePath = doc["config"].ChildNodes[1].Attributes["Path"].Value;   
        }
        void DeleteFile()
        {
            //Delete All File have run today
            DirectoryInfo df = new DirectoryInfo(FilePath);
            FileInfo[] files = df.GetFiles(DateTime.Today.ToString("yyyyMMdd") + "*");
            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(files[i].FullName);
            }
        
        }
        public void ProcessFilmSchedule()
        {
            //DeleteFile(); 
            for (int i = 0; i < Configs.Count; i++)
            { 
                PCRConfig config = (PCRConfig)Configs[i];
                switch (config.Type)
                {
                    case "Megastar":
                        {
                            DownLoadFilmMegaStar mega = new DownLoadFilmMegaStar();
                            mega.LoadConfig(config);
                            mega.FilePath = FilePath; 
                            mega.ProcessFilm();                           
                        }
                        break;
                    case "Galaxy":
                        {
                            DownLoadFilmGalaxy gala = new DownLoadFilmGalaxy();
                            gala.LoadConfig(config);
                            gala.FilePath = FilePath;
                            gala.ProcessFilm();
                        }
                        break;
                }
            }
        }
    }
    
        
}
