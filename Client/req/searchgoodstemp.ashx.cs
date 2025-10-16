using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Collections.Generic;
using SqlDataProvider.Data;
using Bussiness;
using Road.Flash;
using System.IO;
using log4net;
using System.Reflection;

namespace Tank.Request
{
    /// <summary>
    /// Summary description for searchgoodstemp
    /// </summary>
    public class searchgoodstemp : IHttpHandler
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void ProcessRequest(HttpContext context)
        {
            if (csFunction.ValidAdminIP(context.Request.UserHostAddress))
            {
                context.Response.Write(Bulid(context));
            }
            else
            {
                context.Response.Write("IP is not valid!");
            }
        }

        public static string Bulid(HttpContext context)
        {
            bool value = false;
            string message = "Fail!";
            XElement result = new XElement("Result");
            try
            {
                using (ProduceBussiness db = new ProduceBussiness())
                {
                    SearchGoodsTempInfo[] infos = db.GetAllSearchGoodsTemp();
                    foreach (SearchGoodsTempInfo info in infos)
                    {
                        result.Add(FlashUtils.CreateSearchGoodsTemp(info));
                    }
                    value = true;
                    message = "Success!";
                }
            }
            catch (Exception ex)
            {
                log.Error("searchgoodstemp", ex);
            }

            result.Add(new XAttribute("value", value));
            result.Add(new XAttribute("message", message));
            return csFunction.CreateCompressXml(context, result, "searchgoodstemp", false);
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