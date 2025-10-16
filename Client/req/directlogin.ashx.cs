using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Text;
using Bussiness.Interface;
using Road.Flash;

namespace Tank.Request
{
    /// <summary>
    /// Summary description for directlogin
    /// </summary>
    public class directlogin : IHttpHandler
    {
        public string PHP_Key
        {
            get
            {
                return ConfigurationManager.AppSettings["PHP_Key"];
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string result = "";
            string name = context.Request["username"];
            string phpkey = context.Request["phpkey"];
            string password = Guid.NewGuid().ToString();
            string time = BaseInterface.ConvertDateTimeInt(DateTime.Now).ToString();
            string key = string.Empty;
            if (phpkey == PHP_Key)
            {                
                if (string.IsNullOrEmpty(key))
                {
                    key = BaseInterface.GetLoginKey;
                }
                string v = BaseInterface.md5(name + password + time.ToString() + key);
                string Url = (BaseInterface.LoginUrl + "?content=" + HttpUtility.UrlEncode(name + "|" + password + "|" + time.ToString() + "|" + v));
                result = BaseInterface.RequestContent(Url);
                if (result == "0")
                {
                    context.Response.Write(password.ToUpper());
                }
                else
                {
                    context.Response.Write("0");
                }
            }
            else
            {
                context.Response.Write("2");
            }       
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}