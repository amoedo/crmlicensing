using CrmLicensing.CrmPackageByKey.Plugins.Licensing;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmLicensing.CrmPackageByKey.Plugins
{
    public abstract class LicensedPlugin : Plugin
    {
        public LicensedPlugin(Type type)
            : base(type)
        { }

        /// <summary>
        /// Checks if there is a valid license. 
        /// Throws <typeparamref name="InvalidPluginExecutionException"/> if license is not valid
        /// </summary>
        /// <param name="localContext"></param>
        protected static void DoLicenseCheck(LocalPluginContext localContext)
        {
            localContext.Trace("Begin checking for license");
            if (!LicenseChecked)
            {

                if (!LicenseChecker.CheckLicense(localContext.OrganizationService, localContext.PluginExecutionContext.OrganizationName))
                {
                    localContext.Trace(String.Format("Not valid License Found for Org:{0}", localContext.PluginExecutionContext.OrganizationName));
                    throw new InvalidPluginExecutionException("Not Valid License found for VatChecker solution");
                }
                else
                {
                    localContext.Trace("License Valid");
                    LicenseChecked = true;
                }
            }
        }

        /// <summary>
        /// To Allow caching of license Check
        /// </summary>
        protected static bool LicenseChecked { get; set; }
    }
}
