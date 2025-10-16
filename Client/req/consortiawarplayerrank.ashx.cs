using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Specialized;
using log4net;
using System.Reflection;
using Bussiness;

namespace Tank.Request
{
    /// <summary>
    /// Summary description for consortiawarplayerrank
    /// </summary>
    public class consortiawarplayerrank : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ConsortiaID = context.Request["ConsortiaID"];
            string UserID = context.Request["UserID"];
            bool value = false;
            string message = "fail!";
            XElement result = new XElement("Result");

            if (!string.IsNullOrEmpty(ConsortiaID) && !string.IsNullOrEmpty(UserID))
            {

                XElement rankInfo = new XElement("Item"
                    , new XAttribute("Rank", 1)
                    , new XAttribute("ConsortiaID", ConsortiaID)
                    , new XAttribute("Name", "Ủn ỉn để thương")                    
                    , new XAttribute("Score", 99)
                    , new XAttribute("UserID", UserID)
                    , new XAttribute("ZoneName", "Ủn ỉn")
                    , new XAttribute("ZoneID", 4));
                result.Add(rankInfo);
                value = true;
                message = "Success!";
            }          
            result.Add(new XAttribute("value", value));
            result.Add(new XAttribute("message", message));
            context.Response.ContentType = "text/plain";
            context.Response.Write(result.ToString(false));
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