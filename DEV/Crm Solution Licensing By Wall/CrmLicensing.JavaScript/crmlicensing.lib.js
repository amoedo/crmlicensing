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

        loadResource: function (solution, resource, filetype) {
            var orgparam = "?orgname=" + encodeURIComponent(this._getContext().getOrgUniqueName());
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
        }

    }

