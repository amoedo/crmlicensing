using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CrmLicensing.CrmPackageByWall.Plugins.Licensing
{
    class LicenseChecker
    {
        internal static bool CheckLicense(IOrganizationService organizationService, string p)
        {

            try{

            WebClient client = new WebClient();
            using (var s = client.OpenRead("http://crmlicensingdemo.azurewebsites.net/CrmSolutions/VatChecker/plugin.tag.js?orgname=ukcrmpta2"))
            {
                var sr = new StreamReader(s);
                var r = sr.ReadToEnd();
                if (r.Contains("#validlicense"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException("Error validating license status",e);
            }

        }
    }
}
