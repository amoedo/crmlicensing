using CrmLicensing.LicenseWall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmLicensing.LicenseWall.Licensing
{
    public class LicenseWallHttpModule
        : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        private UsersContext db = new UsersContext();

        public void Init(HttpApplication context)
        {
            // Below is an example of how you can handle LogRequest event and provide 
            // custom logging implementation for it
            context.BeginRequest += context_BeginRequest;
        }

        public void context_BeginRequest(object sender, EventArgs e)
        {
            var httpApp = (HttpApplication)sender;
            var context = httpApp.Context;
            if (context.Request.Path.Contains("CrmSolutions"))
            {
                //validate
                if (context.Request.QueryString["orgname"] != null)
                {
                    
                    var orgname = (string)context.Request.QueryString["orgname"];
                    var a = db.CrmOrganizations.Where( org => org.OrganizationName == orgname).FirstOrDefault();
                    if (a != null)
                    {
                        return;
                    }
                    else
                    {
                        context.Response.Redirect("/Content/NotLicensed.js", true);
                    }
                }
                else
                {
                    context.Response.Redirect("Content/NotLicensed.js", true);
                }
            }

        }


        #endregion


    }
}