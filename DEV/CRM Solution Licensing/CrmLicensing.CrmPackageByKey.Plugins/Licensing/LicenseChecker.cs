using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CrmLicensing.CrmPackageByKey.Plugins.Licensing
{
    class LicenseChecker
    {
        const string PublicKey = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><RSAKeyValue><Modulus>lPoH4/6j6M2zvEsSGr/NIwsSvCGKG+YVIuBoLnavqDYXCphL6N+VpdwBcaa5Q1miMavZ2nY/s1GFCiO1YjyDop3B8MKpbyHXK8vs/c1KNsLtIFkQaXkyN0t2LxCzweGZHidB09O1Zvh9l7jIwgrrbBX+tSmsZv/mgo/w/1A1wgx1N7rGf/CIhLfPQI2hqRchRtzP41XDFej4IBskLucQyHKvp6ue7hDlVF0duXX9P5IOJWujGeoJPtewRzUkds+MM9ac5pgnzpd1vMbiAEJVtXYtZ297o1eZvRszazevASNO+GRyojtXs+nDWDugGrkzsyjoy4EPQqdxZg3mrIqANQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        const string Prefix = "clbk_";

        public static bool CheckLicense(IOrganizationService service, string orgName)
        {
            QueryByAttribute query = new QueryByAttribute("webresource");
            query.AddAttributeValue("name", string.Format("{0}/{1}.lic", Prefix, orgName));
            query.ColumnSet = new ColumnSet("content");

            try
            {
                var result = service.RetrieveMultiple(query).Entities.First();
                var xmlLicense = DecodeFrom64((string)result["content"]);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlLicense);
                var lic = doc.GetElementsByTagName("payload")[0].InnerText;
                var sig = doc.GetElementsByTagName("signature")[0].InnerText;

                var csp = new RSACryptoServiceProvider();
                csp.FromXmlString(PublicKey);

                if (csp.VerifyData(
                    System.Text.Encoding.UTF8.GetBytes(lic.ToCharArray(), 0, lic.Length) // We check agains the orgname
                    , "SHA256"
                    , Convert.FromBase64String(sig)))
                {
                    //Check orgname
                    var parts = lic.Split(':');
                    if (parts[0] != orgName)
                    {
                        //Not valid org
                        return false;
                    }
                    else
                    {
                        //Check trial
                        if (parts[1] == "1")
                        {
                            var date = parts[2].Split('/');
                            var expiry = new DateTime(int.Parse(date[2]),int.Parse(date[1]),int.Parse(date[0]));
                            if (expiry < DateTime.Now)
                            {
                                //Expired trial
                                return false;
                            }
                            else
                            {
                                //Valid trial
                                return true;
                            }
                        }
                        else
                        {
                            //Valid full license
                            return true;
                        }

                    }                    
                }
                else
                {
                    //Signature not valid
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
