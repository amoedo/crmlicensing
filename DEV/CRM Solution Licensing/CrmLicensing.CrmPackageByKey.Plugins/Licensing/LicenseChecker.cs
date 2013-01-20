using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CrmLicensing.CrmPackageByKey.Plugins
{
    class LicenseChecker
    {
        const string PublicKey = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><RSAKeyValue><Modulus>wa9IopimOHtUqeKfY5x06+pOL4wV4zBUxqPfDrFPCW1IfHIvND/sqiemNRss81iiCiid4B0leREJ1Aamu11kDX3RVoNZ0QCZEW8ZF5vCw+KGhU2kTqFte7TRwvIB84BlE86XCr7GOix9ZbFEW6Ewg/vLlJCY3VELkFr/lUEDWhzgYDohHBlrjUtwHx+PUd70xykMEelrOShP2NuIWGlcCpO6/omEVvAOmrm5T2HJp8o3ajr02i1GTA/glms0+kVmoJWjGMUMRAuu6bD9GuTVdW5ZKKWzD8QuUq4GkrueUgskxzoQJ4aATHzmYXHJORrDaVQydRffM5BNEe4YOxEPiQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        const string Prefix = "clbk_";

        public static bool CheckLicense(IOrganizationService service, string orgName)
        {
            QueryByAttribute query = new QueryByAttribute("WebResource");
            query.AddAttributeValue("name", string.Format("{0}/{1}.lic", Prefix, orgName));
            query.ColumnSet = new ColumnSet("content");

            try
            {
                var result = service.RetrieveMultiple(query).Entities.First();
                var xmlLicense = DecodeFrom64((string)result["content"]);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmllicense);
                var lic = doc.GetElementsByTagName("license")[0].InnerText;

                var csp = new RSACryptoServiceProvider();
                csp.FromXmlString(PublicKey);

                if (csp.VerifyData(
                    System.Text.Encoding.UTF8.GetBytes(orgName.ToCharArray(), 0, orgName.Length) // We check agains the orgname
                    , "SHA256"
                    , Convert.FromBase64String(lic)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(String.Format("Error retrieving license file for {0}. Error:{1}", orgName, e.Message));
            }

        }

        static public string DecodeFrom64(string encodedData)
        {

            byte[] encodedDataAsBytes

                = System.Convert.FromBase64String(encodedData);

            string returnValue =

               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;

        }

    }
}
