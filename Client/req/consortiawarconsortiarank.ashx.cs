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
    /// Summary description for consortiawarconsortiarank
    /// </summary>
    public class consortiawarconsortiarank : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            
             bool value = false;
            string message = "fail!";
            XElement result = new XElement("Result");
                XElement rankInfo = new XElement("Item"
                    , new XAttribute("Rank", 1)
                    , new XAttribute("ConsortiaID", 1)
                    , new XAttribute("Name", "Ủn ỉn Guild")
                    , new XAttribute("Score", 9999));
                result.Add(rankInfo);
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