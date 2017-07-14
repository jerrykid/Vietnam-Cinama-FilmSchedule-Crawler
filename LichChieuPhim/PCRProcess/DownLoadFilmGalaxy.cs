using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Data;
using System.Collections; 

namespace PCRProcess
{
    public class DownLoadFilmGalaxy: FilmCinema 
    {       

        public ArrayList GetListFilm(string value)
        {
            ArrayList result = new ArrayList();
            Seed = 0;
            Start = 1;
            End = 2;
            Start = value.IndexOf("<div class=\"subject-common\">", Seed);
            End = value.IndexOf("</div>", Start);
            CinemaName = value.Substring(Start + 28, End - Start - 28);
            Start = value.IndexOf("<span class=\"subject-common color-brown\">", Seed);
            End = value.IndexOf("</span>", Start);           
            Seed = Start;
            bool isStop = false;
            int length = value.Length; 
            while (End > Start && Start > 0 && !isStop)
            {
                //Get film name
                isStop = true;
                if (Seed <length && value.IndexOf("<span class=\"subject-common color-brown\">",Seed,210) > 0)
                {
                    Start = value.IndexOf("<span class=\"subject-common color-brown\">",Seed);
                    End = value.IndexOf("</span>", Start);
                    FilmName = RemoveCarrieReturn(value.Substring(Start + 41, End - Start - 41 ));
                    End += 60;
                    Start = End;
                    Seed = End;
                    End++;
                    isStop = false;
                }

                //Get Date 
                if (Seed < length && value.IndexOf("<div class=\"subject-content \">", Seed, 210) > 0)
                {
                    Start = value.IndexOf("<div class=\"subject-content \">", Seed, 210);
                    End = value.IndexOf("</div>", Start);
                    Date = RemoveCarrieReturn(value.Substring(Start + 30, End - Start - 30));
                    End += 60;
                    Start = End;
                    Seed = End;
                    End++;
                    isStop = false;
                }

                if (Seed < length &&  value.IndexOf("/vi/booking?", Seed, 210) > 0)
                {
                    Start = value.IndexOf("/vi/booking?", Seed, 210);
                    End = value.IndexOf("\"", Start);
                    Link = WebLink + value.Substring(Start, End - Start );
                    Start = End + 21;
                    End = value.IndexOf("</a>",Start);
                    Time =  RemoveCarrieReturn(value.Substring(Start, End - Start));
                    End += 60;
                    Start = End;
                    Seed = End;
                    End++;
                    result.Add(CinemaCodes[currentID] + Splitter + CinemaName + Splitter + FilmName + Splitter + Date + Splitter + Time + Splitter + Link);
                    isStop = false;
                }              

            }
            return result; 
        }

        int currentID = 0;
        string[] CinemaCodes;
        public void ProcessFilm()
        {
            CinemaCodes = CinemaCode.Split(',');  
            string value = LoadFromWeb(CinemaLink);
            string cinemaContent = string.Empty;
            int start=0, end = 0;
            start = value.IndexOf("<div class=\"subject-common\">", end);
            end = value.IndexOf("<div class=\"sep10 p10\"></div>", Start);
            while (start > 0 && end > start)
            {
                cinemaContent = value.Substring(start, end - start );
                cinemaContent += EmptyRow; 
                if (start > 0 && end > 0) {
                    ArrayList listFilm = GetListFilm(cinemaContent);
                    currentID++;
                    WriteToCSV(listFilm);
                    start = end;
                    end++;
                }
                start = value.IndexOf("<div class=\"subject-common\">", end);
                if (start > 0)
                {
                    end = value.IndexOf("<div class=\"sep10 p10\"></div>", start);
                }
             
   
            }
            
        }
    }
}
