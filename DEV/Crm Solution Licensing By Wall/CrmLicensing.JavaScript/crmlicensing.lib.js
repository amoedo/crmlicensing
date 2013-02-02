if (typeof (CLBW) == "undefined")
{ CLBW = { __namespace: true }; }
//This will establish a more unique namespace for functions in this library. This will reduce the 
// potential for functions to be overwritten due to a duplicate name when the library is loaded.
CLBW.LicensedResource =
    {
        _getContext: function () {

            if (typeof GetGlobalContext == "function") {
                return GetGlobalContext();
            }
            else {
                if (typeof Xrm.Page.context == "object") {
                    return Xrm.Page.context;
                }
                else { throw new Error("Unable to access context"); }
            }
            return null;
        },

        // Function to inject the appropiate element to load a license resources
        loadResource: function (solution, resource, filetype,avoidcached) {
            var orgparam = "?orgname=" + encodeURIComponent(this._getContext().getOrgUniqueName());
            var dt = new Date();
            if (avoidcached) orgparam += "&rand="+dt.getTime();
            else orgparam += "&rand="+dt.getDate()+dt.getMonth();

            var filename = CLBW.BaseUrl + solution + "/" + resource + orgparam;

            if (filetype == "js") { //if filename is a external JavaScript file
                var fileref = document.createElement('script')
                fileref.setAttribute("type", "text/javascript")
                fileref.setAttribute("src", filename);
            }
            else if (filetype == "css") { //if filename is an external CSS file
                var fileref = document.createElement("link")
                fileref.setAttribute("rel", "stylesheet")
                fileref.setAttribute("type", "text/css")
                fileref.setAttribute("href", filename)
            }
            if (typeof fileref != "undefined")
                document.getElementsByTagName("head")[0].appendChild(fileref)
        },

        // function to check whether the organization is licensed
        _callLicenseServer: function (solution) {
            try
            {
                this.loadResource(solution, CLBW.LicenseTagFile, "js",true);
            }
            catch (e) {                
                this.callback(false,e.message);
            }
        },

        // call the license checker
        // Param: Solution = CRM solution name to check
        // Param: Callback = Function to call back whit the result function (bool result, exception e)

        checkLicense: function (solution, callback) {

            this.callback = callback;
            this._callLicenseServer(solution);

        },

        callback: function (licensed, error) {
            if (!licensed)
            {
                try
                {
                    document.body.innerHTML ="<h1>Not Licensed</h1>";
                    if (error) document.body.innerHTML +="<p>"+error+"</p>";
                }
                catch (e) {
                    alert("License Not Valid "+ error);
                }
            }
        }
    }

