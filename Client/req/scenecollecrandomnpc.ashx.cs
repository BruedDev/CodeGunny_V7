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
using SqlDataProvider.Data;
using Road.Flash;


namespace Tank.Request
{
    /// <summary>
    /// Summary description for scenecollecrandomnpc
    /// </summary>
    public class scenecollecrandomnpc : IHttpHandler
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public void ProcessRequest(HttpContext context)
        {
            bool value = false;
            string message = "fail!";
            XElement result = new XElement("Result");
            try
            {
                using (PlayerBussiness db = new PlayerBussiness())
                {
                    //SubActiveInfo[] Actives = db.GetAllSubActive();
                    //foreach (SubActiveInfo Active in Actives)
                    {
                        result.Add(FlashUtils.CreatescenecollecrandomnpcInfo());                       
                    }
                    value = true;
                    message = "Success!";
                }
            }
            catch (Exception ex)
            {
                log.Error("subactivelist", ex);
            }
            result.Add(new XAttribute("value", value));
            result.Add(new XAttribute("message", message));
            context.Response.ContentType = "text/plain";
            context.Response.BinaryWrite(StaticFunction.Compress(result.ToString(false)));
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