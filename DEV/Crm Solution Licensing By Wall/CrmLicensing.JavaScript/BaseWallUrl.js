if (typeof (CLBW) == "undefined")
{ CLBW = { __namespace: true }; }
//This will establish a more unique namespace for functions in this library. This will reduce the 
// potential for functions to be overwritten due to a duplicate name when the library is loaded.

// Base URL for the CRM Solutions behind the License Wall
CLBW.BaseUrl = "https://crmlicensingdemo.azurewebsites.net/CrmSolutions/";

// Market file withing the solution folder with the #validlicense tag
CLBW.LicenseTagFile = "license.tag.js";