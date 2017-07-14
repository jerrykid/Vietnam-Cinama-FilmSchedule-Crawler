using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Data;
using System.Collections;
using System.Web; 

namespace PCRProcess
{
    public class DownLoadFilmMegaStar : FilmCinema
    {

        public void GetFilmDetail(string url)
        {
            string value = LoadFromWeb(url);
            ArrayList result = GetFilmSchedule(value);
            WriteToCSV(result);
        }

        public ArrayList GetListFilm()
        {
            string value = LoadFromWeb(CinemaLink);
            ArrayList result = new ArrayList();
            Start = value.IndexOf("<ul data-role=\"listview\" class=\"list-film-2\">");
            End = value.IndexOf("</ul>", Start);
            Seed = 0;
            value = value.Substring(Start, End - Start);
            while (Start > 0 && End > Start)
            {
                Start = value.IndexOf("<a href=\"", Seed);
                if (Start > 0)
                {
                    End = value.IndexOf("\">", Start);
                    result.Add(value.Substring(Start + 9, End - Start - 9));
                    Seed = End;
                }

            }
            return result;
        }


        public ArrayList GetFilmSchedule(string value)
        {
            ArrayList result = new ArrayList();            
            Start = value.IndexOf("<span class=\"orange-13\">");
            End = value.IndexOf("</span>", Start);
            Seed = 0;
            Date = RemoveCarrieReturn(value.Substring(Start + 24, End - Start - 24));
            while (Start > 0 && End > Start)
            {
                Start = value.IndexOf("https://www.megastar.vn/mobile/visLtyTicketsLogin.aspx", Seed);
                if (Start > 0)
                {
                    End = value.IndexOf("\");'>", Start);
                    Link = RemoveCarrieReturn(value.Substring(Start, End - Start));
                    FilmName = UrlHelper.Decode(Link.Substring(Link.LastIndexOf("MovieName=") + 10).Replace('+', ' '));
                    Start = End + 5;
                    End = value.IndexOf("<", Start);
                    Time = value.Substring(Start, End - Start);
                    result.Add(CinemaCode + Splitter + CinemaName + Splitter + FilmName + Splitter + Date + Splitter + Time + Splitter + Link);
                    Start = End;
                    Seed = End;
                    End = Start + 1;
                    Link = string.Empty;
                    Time = string.Empty;
                }
                if (value.IndexOf("<span class=\"orange-13\">", Seed, 120) > 0)
                {
                    Start = value.IndexOf("<span class=\"orange-13\">", Seed, 120);
                    End = value.IndexOf("</span>", Start);
                    Date = RemoveCarrieReturn(value.Substring(Start + 24, End - Start - 24));
                    Seed = End;
                    Start = End;
                    End++;
                }
            }
            return result;
        }
        public void ProcessFilm()
        {
            ArrayList Links = GetListFilm();
            for (int i = 0; i < Links.Count; i++)
            {
                GetFilmDetail(WebLink +  Links[i].ToString() );
            }
        }
    }
}
