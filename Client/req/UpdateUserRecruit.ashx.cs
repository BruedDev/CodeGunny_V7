using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tank.Request
{
    /// <summary>
    /// Summary description for UpdateUserRecruit
    /// </summary>
    public class UpdateUserRecruit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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