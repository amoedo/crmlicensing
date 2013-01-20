if (typeof (CLBK) == "undefined")
{ CLBK = { __namespace: true }; }
//This will establish a more unique namespace for functions in this library. This will reduce the 
// potential for functions to be overwritten due to a duplicate name when the library is loaded.
CLBK.VatChecker =
    {
        _getServerUrl: function () {
            return "http://ec.europa.eu";
        },

        _getSoap: function (countryCode, vatNumber) {
            var requestMain = ""
            requestMain += "<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">";
            requestMain += "  <soap:Body>";
            requestMain += "    <checkVat xmlns=\"urn:ec.europa.eu:taxud:vies:services:checkVat:types\">";
            requestMain += "      <countryCode>" + countryCode + "</countryCode>";
            requestMain += "      <vatNumber>" + vatNumber + "</vatNumber>";
            requestMain += "    </checkVat>";
            requestMain += "  </soap:Body>";
            requestMain += "</soap:Envelope>";
            return requestMain;
        },

        checkVat: function (countryCode, vatNumber, name, address) {

            $.support.cors = true;

            $.ajax({
                crossDomain: true,
                type: "POST",
                contentType: "text/xml; charset=utf-8",
                url: this._getServerUrl() + "/taxation_customs/vies/services/checkVatService",
                data: this._getSoap(countryCode, vatNumber),
                beforeSend: function (XMLHttpRequest) {
                    XMLHttpRequest.setRequestHeader("Accept", "application/xml");
                    XMLHttpRequest.setRequestHeader("SOAPAction", "http://schemas.microsoft.com/xrm/2011/Contracts/Services/IOrganizationService/Execute");
                },
                success: function (data, textStatus, XmlHttpRequest) {
                    //check results;

                    return true;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }
    }
