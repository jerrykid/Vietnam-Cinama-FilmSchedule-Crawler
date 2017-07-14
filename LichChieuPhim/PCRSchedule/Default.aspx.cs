using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PCRProcess;
using System.IO;
namespace PCRSchedule
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadFiles();
            CheckSchedule();

        }


        protected void LoadFiles()
        {
            
            PCRProcess.PCRProcess pcr = new PCRProcess.PCRProcess();
            pcr.LoadConfig(Server.MapPath("Config.xml"));
            DirectoryInfo df = new DirectoryInfo(pcr.FilePath);
            FileInfo []files =  df.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                HyperLink link = new HyperLink();                 
                link.Text = files[i].Name;
                link.NavigateUrl = "Data/" +  files[i].Name;
                divListSchedule.Controls.Add(link);
                divListSchedule.Controls.Add(new LiteralControl("<br />"));    
                
            }
        }

        protected void btnGetSchedule_Click(object sender, EventArgs e)
        {
            Run(); 
        }
        protected void CheckSchedule()
        {
            string value = Request.QueryString["act"];
            if (!string.IsNullOrEmpty(value) && value == "run")
            {
                Run(); 
            }
        }
        protected void Run()
        {
            PCRProcess.PCRProcess pcr = new PCRProcess.PCRProcess();
            pcr.LoadConfig(Server.MapPath("Config.xml"));
            pcr.ProcessFilmSchedule();          
        }
    }
}
