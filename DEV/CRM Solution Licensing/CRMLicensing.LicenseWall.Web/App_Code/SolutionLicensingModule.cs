using System;
using System.Web;

namespace CRMLicensing.LicenseWall.Web.App_Code
{
    public class SolutionLicensingModule : IHttpModule
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
            }

        }


        #endregion

    }
}
