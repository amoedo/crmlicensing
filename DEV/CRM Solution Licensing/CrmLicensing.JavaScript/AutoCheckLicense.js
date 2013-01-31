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
        _checkOrgName: function (orgName)
        {
            return orgName == this._getContext().getOrgUniqueName();
        },
        _doVerify: function (license, key) {

            try {

                var licenseDoc = $.parseXML(license);
                
                var sMsg = licenseDoc.getElementsByTagName("payload")[0].childNodes[0].nodeValue
                var hSig = licenseDoc.getElementsByTagName("signature")[0].childNodes[0].nodeValue;

                var rsa = new RSAKey();
                rsa.loadPublicKeyFromXml(key);

                hSig = b64tohex(hSig);

                var isValid = rsa.verifyString(sMsg, hSig);

                // display verification result
                if (isValid) {
                    //Get parsts
                    var components = sMsg.split(":");
                    //Check Org Name
                    if (!this._checkOrgName(components[0])) {
                        alert("Not Valid License");
                        window.document.body.innerHTML = "<h1>License organisation name missmatch</h1>";
                    }
                    else
                    {
                        //Check for trial
                        if (components[1] == "1")
                        {
                            //Check expiry date
                            var date = components[2].split("/");
                            var expiry = new Date(date[2], date[1]-1, date[0]); //month is 0-11
                            if (expiry < new Date())
                            {
                                try
                                {
                                    alert("Trial has expired");
                                }
                                catch (e) { }
                                window.document.body.innerHTML = "<h1>Trial License expired</h1>";
                            }

                        }
                    }

                } else {
                    alert("Not Valid License");
                    window.document.body.innerHTML = "<h1>License signature not valid</h1>";
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
                        CLBK.LicenseChecker.Validate(data.xml);
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