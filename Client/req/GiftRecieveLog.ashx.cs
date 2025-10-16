using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Xml.Linq;
using Road;
using System.Security.Cryptography;
using System.Configuration;
using System.Text;
using Road.Flash;
using System.Web.SessionState;
using Bussiness;
using SqlDataProvider.Data;
using log4net;
using System.Reflection;
using Bussiness.Interface;

namespace Tank.Request
{
    /// <summary>
    /// Summary description for GiftRecieveLog
    /// </summary>
    public class GiftRecieveLog : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            bool value = false;
            string message = "Fail!";
            XElement result = new XElement("Result");

            try
            {
                //using (ProduceBussiness db = new ProduceBussiness())
                //{
                //    DailyAwardInfo[] infos = db.GetAllDailyAward();
                //    foreach (DailyAwardInfo info in infos)
                //    {
                //        result.Add(FlashUtils.CreateActiveInfo(info));
                //    }

                value = true;
                message = "Success!";
                //}
            }
            catch (Exception ex)
            {
                //log.Error("Load GiftRecieveLog is fail!", ex);
            }
            finally
            {
                result.Add(new XAttribute("value", value));
                result.Add(new XAttribute("message", message));
                context.Response.ContentType = "text/plain";
                //context.Response.Write(result.ToString(false));
                context.Response.BinaryWrite(StaticFunction.Compress(result.ToString(false)));
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