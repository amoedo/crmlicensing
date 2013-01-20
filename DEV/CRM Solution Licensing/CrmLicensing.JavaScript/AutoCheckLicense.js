if (typeof (CLBK) == "undefined")
{ CLBK = { __namespace: true }; }
//This will establish a more unique namespace for functions in this library. This will reduce the 
// potential for functions to be overwritten due to a duplicate name when the library is loaded.
CLBK.LicenseChecker =
    {
        _prefix: "clbk_",
        _getServerUrl: function () {
            var serverUrl = "";
            if (typeof GetGlobalContext == "function") {
                var context = GetGlobalContext();
                serverUrl = context.getServerUrl();
            }
            else {
                if (typeof Xrm.Page.context == "object") {
                    serverUrl = Xrm.Page.context.getServerUrl();
                }
                else { throw new Error("Unable to access the server URL"); }
            }
            if (serverUrl.match(/\/$/)) {
                serverUrl = serverUrl.substring(0, serverUrl.length - 1);
            }
            return serverUrl;
        },
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
        _doVerify: function (license, key) {

            try {

                debugger;

                var sMsg = this._getContext().getOrgUniqueName();
                var hSig = license;

                var rsa = new RSAKey();
                rsa.loadPublicKeyFromXml(key);

                hSig = b64tohex(hSig);

                var isValid = rsa.verifyString(sMsg, hSig);

                // display verification result
                if (isValid) {
                    //do nothing. 
                    //Could be extended to check other items
                } else {
                    alert("Not Valid License");
                    window.document.body.innerHTML = "<h1>License not valid</h1>";
                }
            }
            catch (e) {
                alert("Not Valid License");
                window.document.body.innerHTML = "<h1>License not valid</h1>" + "<p>" + e.message + "</p";
            }
        },
        Validate: function (license) {
            try {
                var url = this._getServerUrl() + "/WebResources/" + this._prefix + "/publickey.xml";

                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    datatype: "text/xml",
                    url: url,
                    beforeSend: function (XMLHttpRequest) {
                    },
                    success: function (data, textStatus, XmlHttpRequest) {
                        CLBK.LicenseChecker._doVerify(license, data.xml);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        window.document.body.innerHTML = "<h1>License validation error</h1>";
                    }
                });

            }
            catch (e) {
                window.document.body.innerHTML = "<h1>License validation error</h1>" + "<p>" + e.message + "</p";
            }

        },
        Check: function () {
            try {
                var url = this._getServerUrl() + "/WebResources/" + this._prefix + "/" + this._getContext().getOrgUniqueName() + ".lic";

                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    datatype: "text/xml",
                    url: url,
                    beforeSend: function (XMLHttpRequest) {
                    },
                    success: function (data, textStatus, XmlHttpRequest) {
                        //TODO
                        CLBK.LicenseChecker.Validate(data.text);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        window.document.body.innerHTML = "<h1>License not present</h1>";
                    }
                });
            }
            catch (e) {
                window.document.body.innerHTML = "<h1>License not present</h1>" + "<p>" + e.message + "</p";
            }
        }


    }


CLBK.LicenseChecker.Check();