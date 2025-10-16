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
    /// Summary description for subactivelist
    /// </summary>
    public class subactivelist : IHttpHandler
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
                    SubActiveInfo[] Actives = db.GetAllSubActive();
                    foreach (SubActiveInfo Active in Actives)
                    {
                        result.Add(FlashUtils.CreateActiveInfo(Active));
                        SubActiveConditionInfo[] Conditions = db.GetAllSubActiveCondition(Active.ActiveID);
                        foreach (SubActiveConditionInfo Condition in Conditions)
                        {
                            result.Add(FlashUtils.CreateActiveConditionInfo(Condition));
                        }
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
            result.Add(new XAttribute("nowTime", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
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