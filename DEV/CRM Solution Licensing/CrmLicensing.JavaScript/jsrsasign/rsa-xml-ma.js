/// <reference path="rsa.js" />
/// <reference path="rsa2.js" />
/// <reference path="rsasign-1.2.js" />
/// <reference path="base64.js" />


function _loadPublicKeyFromXml(xmlKey) {

    var xmlDoc;
    if (typeof(xmlKey)=="string")
    {
        if (window.DOMParser) {
            parser = new DOMParser();
            xmlDoc = parser.parseFromString(xmlKey, "text/xml");
        }
        else // Internet Explorer
        {
            xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async = false;
            xmlDoc.loadXML(xmlKey);
        }
    }
    else
    {
        xmlDoc = xmlKey;
    }

    var nBase64 = xmlDoc.getElementsByTagName("Modulus")[0].textContent;
    if (!nBase64) nBase64 = xmlDoc.getElementsByTagName("Modulus")[0].text;
    var eBase64 = xmlDoc.getElementsByTagName("Exponent")[0].textContent;
    if (!eBase64) eBase64 = xmlDoc.getElementsByTagName("Exponent")[0].text;

    var n = b64tohex(nBase64);
    var e = b64tohex(eBase64);

    this.setPublic(n, e);
}

RSAKey.prototype.loadPublicKeyFromXml = _loadPublicKeyFromXml;