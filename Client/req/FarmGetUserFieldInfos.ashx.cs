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

namespace Tank.Request
{
    /// <summary>
    /// Summary description for FarmGetUserFieldInfos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FarmGetUserFieldInfos : IHttpHandler
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static int AccelerateTimeFields(DateTime PlantTime, int FieldValidDate)
        {

            DateTime _now = DateTime.Now;
            int validH = _now.Hour - PlantTime.Hour;
            int validM = _now.Minute - PlantTime.Minute;
            int AccelerateTime = 0;

            if (validH < 0)
            {
                validH = 24 + validH;
            }
            if (validM < 0)
            {
                validM = 60 + validM;
            }
            AccelerateTime = (validH * 60) + validM;
            if (AccelerateTime > FieldValidDate)
            {
                AccelerateTime = FieldValidDate;
            }
            return AccelerateTime;
        }
        private static int AccelerateTimeFields(UserFieldInfo m_field)
        {
            int m_time = 0;
            if (m_field != null)
            {
                if (m_field.SeedID > 0)
                {
                    DateTime PlantTime = m_field.PlantTime;
                    int FieldValidDate = m_field.FieldValidDate;
                    m_time = AccelerateTimeFields(PlantTime, FieldValidDate);
                }
            }
            return m_time;
        }
        public void ProcessRequest(HttpContext context)
        {            
            int selfid = Convert.ToInt32(context.Request["selfid"]);
            string key = context.Request["key"];
            bool value = true;

            string message = "Success!";
            XElement result = new XElement("Result"); 
            using (PlayerBussiness db = new PlayerBussiness())
            {
                FriendInfo[] infos = db.GetFriendsAll(selfid);

                foreach (FriendInfo g in infos)
                {
                    XElement node = new XElement("Item");
                    UserFieldInfo[] fields = db.GetSingleFields(g.FriendID);
                    foreach (UserFieldInfo f in fields)
                    {
                        XElement Item = new XElement("Item",
                            new XAttribute("SeedID", f.SeedID),
                            new XAttribute("AcclerateDate", AccelerateTimeFields(f)),
                            new XAttribute("GrowTime", f.PlantTime.ToString("yyyy-MM-ddTHH:mm:ss")));//"2012-08-21T12:07:48" 
                        node.Add(Item);
                    }
                    node.Add(new XAttribute("UserID", g.FriendID));
                    result.Add(node);
                }
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